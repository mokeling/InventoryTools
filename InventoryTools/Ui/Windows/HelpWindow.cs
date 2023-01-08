using System.Numerics;
using ImGuiNET;
using InventoryTools.Extensions;
using InventoryTools.Logic;

namespace InventoryTools.Ui
{
    public class HelpWindow : Window
    {
        public override bool SaveState => false;
        public static string AsKey => "help";
        public override  string Key => AsKey;
        public override Vector2 DefaultSize { get; } = new Vector2(700, 700);
        public override  Vector2 MaxSize { get; } = new Vector2(2000, 2000);
        public override  Vector2 MinSize { get; } = new Vector2(200, 200);
        public override bool DestroyOnClose => true;

        public HelpWindow(string name = "Allagan Tools - 帮助") : base(name)
        {
            
        }
        public HelpWindow() : base("Allagan Tools - 帮助")
        {
            
        }
        
        public override void Draw()
        {
            if (ImGui.BeginChild("###ivHelpList", new Vector2(150, -1) * ImGui.GetIO().FontGlobalScale, true))
            {
                if (ImGui.Selectable("通用", ConfigurationManager.Config.SelectedHelpPage == 0))
                {
                    ConfigurationManager.Config.SelectedHelpPage = 0;
                }
                if (ImGui.Selectable("过滤器", ConfigurationManager.Config.SelectedHelpPage == 1))
                {
                    ConfigurationManager.Config.SelectedHelpPage = 1;
                }
                if (ImGui.Selectable("关于", ConfigurationManager.Config.SelectedHelpPage == 2))
                {
                    ConfigurationManager.Config.SelectedHelpPage = 2;
                }
                ImGui.EndChild();
            }

            ImGui.SameLine();

            if (ImGui.BeginChild("###ivHelpView", new Vector2(-1, -1), true))
            {
                if (ConfigurationManager.Config.SelectedHelpPage == 0)
                {
                    ImGui.Text("这是一个非常基础的指南，有关更多信息，请参阅 wiki。");
                    if (ImGui.Button("打开Wiki"))
                    {
                        "https://github.com/Critical-Impact/InventoryTools/wiki/1.-Overview".OpenBrowser();
                    }
                    ImGui.Text("插件基本信息：");
                    ImGui.Separator();
                    ImGui.TextWrapped("Allagan Tools会追踪游戏中的库存。它还使您能够高亮物品在所述库存中的位置。");
                    ImGui.TextWrapped("我从Teamcraft那里获得了一些灵感，并完全相信他们的应用程序所提供的物品栏优化理念。");
                    ImGui.TextWrapped("这个插件是为效率而构建的，因此它无法完全完成Teamcraft可以做的所有库存优化，但它正在实现这一目标。");
                    ImGui.NewLine();
                    ImGui.Text("概念：");
                    ImGui.Separator();
                    ImGui.TextWrapped("过滤器：目前您一次只能启用 1 个过滤器。 有 2 种过滤器可用，一种是窗口过滤器，一种是背景过滤器。 当过滤器处于活动状态时，它会启用突出显示并让您看到相关物品。");
                    ImGui.TextWrapped("窗口过滤器：当Allagan Tools窗口可见时，这是用于确定要突出显示的内容的过滤器。");
                    ImGui.TextWrapped("背景过滤器：当Allagan Tools窗口关闭时，这是用于确定要突出显示的内容的过滤器。 最重要的是，可以使用下面列出的命令打开/关闭它。目的是让您可以设置宏来打开/关闭过滤器。");
                    ImGui.NewLine();
                    ImGui.Text("指令：");
                    ImGui.Separator();
                    ImGui.TextWrapped("以下指令将打开/关闭Allagan Tools主过滤器窗口。");
                    ImGui.Text("/allagantools，/inventorytools，/atools，/inv");
                    ImGui.TextWrapped("下面的指令将切换指定<name>的背景过滤器。");
                    ImGui.Text("/atfiltertoggle <name>，/atf <name>，/invf <name>，/ifilter <name>");
                }
                else if (ConfigurationManager.Config.SelectedHelpPage == 1)
                {
                    ImGui.Text("高级过滤：");
                    ImGui.Separator();
                    ImGui.TextWrapped("创建过滤器或搜索过滤器结果时，可以使用一系列运算符使搜索更加具体。可用的运算符取决于您搜索的内容，但目前支持 !、<、>、>=、<=、=。");
                    ImGui.TextWrapped("! - 显示不包含输入内容的任何结果（非） - 适用于文本和数字。");
                    ImGui.TextWrapped("< - 显示任何值小于输入值的结果 - 适用于数字。");
                    ImGui.TextWrapped("> - 显示任何值大于输入值的结果 - 适用于数字。");
                    ImGui.TextWrapped(">= - 显示任何值大于或等于输入值的结果 - 适用于数字。");
                    ImGui.TextWrapped("<= - 显示任何值小于或等于输入值的结果 - 适用于数字。");
                    ImGui.TextWrapped("= - 显示值与输入值完全相同的任何结果 - 适用于文本和数字。");
                    ImGui.TextWrapped("&& 和 || - 分别表示与和或，可用于将运算符链接在一起。");
                }
                else if (ConfigurationManager.Config.SelectedHelpPage == 2)
                {
                    ImGui.Text("关于：");
                    ImGui.Text("这个插件是在我闲暇时间里写的，这是我出于兴趣嗜好而做的，我希望在一段时间内积极发布更新。");
                    ImGui.Text("如果您遇到任何问题，请通过插件安装程序反馈按钮提交反馈。");
                    ImGui.Text("插件Wiki: ");
                    ImGui.SameLine();
                    if (ImGui.Button("打开##WikiBtn"))
                    {
                        "https://github.com/Critical-Impact/InventoryTools/wiki/1.-Overview".OpenBrowser();
                    }
                    ImGui.Text("发现了一个八阿哥？");
                    ImGui.SameLine();
                    if (ImGui.Button("打开##BugBtn"))
                    {
                        "https://github.com/Critical-Impact/InventoryTools/issues".OpenBrowser();
                    }
                    ImGui.Separator();
                    if (ConfigurationManager.Config.TetrisEnabled)
                    {
                        if (ImGui.Button("我不喜欢俄罗斯方块"))
                        {
                            ConfigurationManager.Config.TetrisEnabled = false;
                        }
                    }
                    else
                    {
                        if (ImGui.Button("我喜欢俄罗斯方块"))
                        {
                            ConfigurationManager.Config.TetrisEnabled = true;
                        }
                    }
                    ImGui.NewLine();
                    ImGui.Separator();
                    ImGui.Text("译者的碎碎念：");
                }
                ImGui.EndChild();
            }
        }
        
        public override FilterConfiguration? SelectedConfiguration => null;

        public override void Invalidate()
        {
            
        }
    }
}