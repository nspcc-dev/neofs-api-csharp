using pbr = global::Google.Protobuf.Reflection;

namespace Gogoproto {
    public static partial class GogoReflection {
        public static pbr::FileDescriptor Descriptor {
            get { return descriptor; }
        }

        private static readonly pbr::FileDescriptor descriptor;

        static GogoReflection() {
            byte[] descriptorData = new byte[0];

            descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
                new pbr::FileDescriptor[] { },
                new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] { }));
        }
    }
}