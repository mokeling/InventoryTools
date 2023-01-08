using System.Numerics;
using ImGuiNET;
using ImGuiScene;
using InventoryTools.Logic;

namespace InventoryTools.Ui
{
    public class IntroWindow : Window
    {
        private TextureWrap _allaganToolsIcon;
        public IntroWindow(string name = "Allagan Tools") : base(name)
        {
            SetupWindow();
        }
        
        public IntroWindow() : base("Allagan Tools")
        {
            SetupWindow();
        }
        
        private void SetupWindow()
        {
            _allaganToolsIcon = PluginService.PluginLogic.LoadImage("icon-hor");
        }
        
        public override void Invalidate()
        {
        }

        public static string AsKey => "Intro";
        public override FilterConfiguration? SelectedConfiguration => null;
        public override string Key => AsKey;
        public override bool DestroyOnClose { get; } = true;
        public override void Draw()
        {
            ImGui.BeginChild("LogoLeft", new Vector2(200, 0));
            ImGui.SetCursorPosY(40);
            ImGui.Image(_allaganToolsIcon.ImGuiHandle, new Vector2(200, 200) * ImGui.GetIO().FontGlobalScale);
            ImGui.EndChild();
            ImGui.SameLine();
            ImGui.BeginChild("IntroRight", new Vector2(0, 0));
            ImGui.TextWrapped("欢迎使用Allagan Tools.");
            ImGui.TextWrapped("Allagan Tools 是 Final Fantasy XIV 的插件，以前称为 Inventory Tools。 添加了制作和物品窗口使插件比原来的应用范围更大。");
            ImGui.TextWrapped("现在可以通过指令快捷方式和主窗口打开各种新窗口。");
            ImGui.TextWrapped("可以通过右键单击窗口中的物品来访问插件的大量功能。这包括有关获取物品的方式、采集地点、贩卖位置、配方等信息。");
            ImGui.TextWrapped("物品的数据解析仍在进行中，因此它不会像 Garland Tools 和 Teamcraft 那样全面。 但是，Allagan Tools 会不断更新和改进，以便为您提供最佳体验。");
            ImGui.NewLine();
            if (ImGui.Button("关闭"))
            {
                Close();
            }
            ImGui.SameLine(0, 4);
            if (ImGui.Button("关闭并打开主窗口"))
            {
                Close();
                PluginService.WindowService.OpenWindow<FiltersWindow>(FiltersWindow.AsKey);
            }
            ImGui.EndChild();
        }

        public override Vector2 DefaultSize { get; } = new Vector2(600, 350);
        public override Vector2 MaxSize { get; } = new Vector2(600, 360);
        public override Vector2 MinSize { get; } = new Vector2(600, 360);
        public override bool SaveState => false;

        public override ImGuiWindowFlags? WindowFlags { get; } =
            ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar;
    }
}