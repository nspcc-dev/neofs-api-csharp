SHELL := /bin/bash

PROTO_VERSION=master
PROTO_URL=https://bitbucket.org/nspcc-dev/neofs-proto/get/$(PROTO_VERSION).tar.gz

B=\033[0;1m
G=\033[0;92m
R=\033[0m

os_type=unknown
os_build=x86

ifeq ($(shell uname -s),Darwin)
	os_type=macosx
endif
ifeq ($(shell uname -s),Linux)
	os_type=linux
endif
ifeq ($(shell uname -m),x86_64)
	os_build=x64
endif

PROTO_TOOLS_PATH=${HOME}/.nuget/packages/grpc.tools
PROTO_TOOLS_VERSION=$(shell ls $(PROTO_TOOLS_PATH) | sort -V | tail -n1)
PROTO_TOOLS_BIN=$(PROTO_TOOLS_PATH)/$(PROTO_TOOLS_VERSION)/tools/$(os_type)_$(os_build)/

.PHONY: deps docgen protoc

# Dependencies
deps:
	@printf "${B}${G}⇒ Install Go-dependencies ${R}\n"
	@go mod tidy -v
	@go mod vendor

	@printf "${B}${G}⇒ Cleanup old files ${R}\n"
	@find src/api -depth -type d -empty -delete
	@find src/netmap -depth -type d -empty -delete
	@find src/api -type f -name '*.pb.cs' -exec rm {} \;
	@find src/netmap -type f -name '*.pb.cs' -exec rm {} \;
	@find src/api -type f -name '*Grpc.cs' -exec rm {} \;
	@find src/netmap -type f -name '*Grpc.cs' -exec rm {} \;
	@find src/api -type f -name '*.proto' -not -name '*_test.proto' -exec rm {} \;
	@find src/netmap -type f -name '*.proto' -not -name '*_test.proto' -exec rm {} \;

	@printf "${B}${G}⇒ NeoFS Proto files ${R}\n"
	@mkdir -p vendor/proto
	@curl -sL -o vendor/proto.tar.gz $(PROTO_URL)
	@tar -xzf vendor/proto.tar.gz --strip-components 1 -C vendor/proto
	@for f in `find vendor/proto -type f -name '*.proto' -exec dirname {} \; | sort -u `; do \
		mkdir -p src/api/$$(basename $$f); \
		cp $$f/*.proto src/api/$$(basename $$f)/; \
	done

	@printf "${B}${G}⇒ NeoFS Netmap Proto files ${R}\n"
	@for f in `find vendor/github.com/nspcc-dev/netmap -type f -name '*.proto' -exec dirname {} \; | sort -u `; do \
		cp $$f/*.proto src/netmap/; \
	done

	@printf "${B}${G}⇒ Cleanup ${R}\n"
	@rm -rf vendor/proto
	@rm -rf vendor/proto.tar.gz

# Regenerate documentation for protot files:
docgen: deps
	@mkdir -p ./docs
	@for f in `find src/api -type f -name '*.proto' -exec dirname {} \; | sort -u `; do \
		printf "${B}${G}⇒ Documentation for $$(basename $$f) ${R}\n"; \
		protoc \
			--doc_opt=.github/markdown.tmpl,$${f}.md \
			--proto_path=src/api:vendor:/usr/local/include \
			--doc_out=docs/ $${f}/*.proto; \
	done

# Regenerate proto files:
protoc: deps
	@printf "${B}${G}⇒ Install specific version for gogo-proto ${R}\n"
	@go list -f '{{.Path}}/...@{{.Version}}' -m github.com/gogo/protobuf | xargs go get -v
	@printf "${B}${G}⇒ Install specific version for protobuf lib ${R}\n"
	@go list -f '{{.Path}}/...@{{.Version}}' -m  github.com/golang/protobuf | xargs go get -v
	@printf "${B}${G}⇒ Protoc generate ${R}\n"
	@for f in `find src/api -type f -name '*.proto'`; do \
 		printf "${B}${G}⇒ Processing $$f ${R}\n"; \
 		protoc \
			--plugin=protoc-gen-grpc=$(PROTO_TOOLS_BIN)/grpc_csharp_plugin \
			--csharp_out=$$(dirname $$f) \
			--csharp_opt=file_extension=.pb.cs \
			--grpc_out=$$(dirname $$f) \
			--grpc_opt=no_server \
			--proto_path=src/api:vendor:/usr/local/include \
			$$f; \
 	done
	@for f in `find src/netmap -type f -name '*.proto'`; do \
 		printf "${B}${G}⇒ Processing $$f ${R}\n"; \
 		protoc \
			--plugin=protoc-gen-grpc=$(PROTO_TOOLS_BIN)/grpc_csharp_plugin \
			--csharp_out=$$(dirname $$f) \
			--csharp_opt=file_extension=.pb.cs \
			--grpc_out=$$(dirname $$f) \
			--grpc_opt=no_server \
			--proto_path=src/netmap:vendor:/usr/local/include \
			$$f; \
 	done
