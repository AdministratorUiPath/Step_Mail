using UiPath.CodedWorkflows.DescriptorIntegration;

namespace ステップメール_通販管理システム顧客データ抽出プロジェクト.ObjectRepository
{
    public static class Descriptors
    {
    }
}

namespace ステップメール_通販管理システム顧客データ抽出プロジェクト._Implementation
{
    internal class ScreenDescriptorDefinition : IScreenDescriptorDefinition
    {
        public IScreenDescriptor Screen { get; set; }

        public string Reference { get; set; }

        public string DisplayName { get; set; }
    }

    internal class ElementDescriptorDefinition : IElementDescriptorDefinition
    {
        public IScreenDescriptor Screen { get; set; }

        public string Reference { get; set; }

        public string DisplayName { get; set; }

        public IElementDescriptor ParentElement { get; set; }

        public IElementDescriptor Element { get; set; }
    }
}