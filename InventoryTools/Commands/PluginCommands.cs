using CriticalCommonLib.Services;
using Dalamud.Logging;
using DalamudPluginProjectTemplate;
using DalamudPluginProjectTemplate.Attributes;
using InventoryTools.Ui;

namespace InventoryTools.Commands
{
    public class PluginCommands
    {
        [Command("/allagantools")]
        [Aliases("/atools")]
        [HelpMessage("显示Allagan Tools过滤器窗口。")]
        public void ShowHideInventoryToolsCommand(string command, string args)
        {
            PluginService.WindowService.ToggleFiltersWindow();
        }
 
        [Command("/inv")]
        [Aliases("/inventorytools")]
        [DoNotShowInHelp]
        [HelpMessage("显示Allagan Tools过滤器窗口。")]
        public  void ShowHideInventoryToolsCommand2(string command, string args)
        {
            PluginService.WindowService.ToggleFiltersWindow();
        }


        [Command("/atfiltertoggle")]
        [Aliases("/atf")]
        [HelpMessage("打开/关闭指定的过滤器并关闭任何其他过滤器。")]
        public  void FilterToggleCommand(string command, string args)
        {
            PluginLog.Verbose(command);
            PluginLog.Verbose(args);
            if (args.Trim() == "")
            {
                ChatUtilities.PrintError("您必须输入过滤器的名称。");
            }
            else
            {
                PluginService.FilterService.ToggleActiveBackgroundFilter(args);
            }
        }

        [Command("/invf")]
        [DoNotShowInHelp]
        [HelpMessage("打开/关闭指定的过滤器并关闭任何其他过滤器。")]
        public  void FilterToggleCommandIT(string command, string args)
        {
            FilterToggleCommand(command, args);
        }

        [Command("/ifilter")]
        [DoNotShowInHelp]
        [HelpMessage("打开/关闭指定的过滤器并关闭任何其他过滤器。")]
        public  void FilterToggleCommand3(string command, string args)
        {
            PluginLog.Verbose(command);
            PluginLog.Verbose(args);
            if (args.Trim() == "")
            {
                ChatUtilities.PrintError("您必须输入过滤器的名称。");
            }
            else
            {
                PluginService.FilterService.ToggleActiveBackgroundFilter(args);
            }
        }

        [Command("/openfilter")]
        [HelpMessage("将指定的过滤器切换为它自己的窗口。")]
        public  void OpenFilterCommand(string command, string args)
        {
            if (args.Trim() == "")
            {
                ChatUtilities.PrintError("您必须输入过滤器的名称。");
            }
            else
            {
                PluginService.PluginLogic.ToggleWindowFilterByName(args);
            }
        }

        [Command("/crafts")]
        [HelpMessage("打开Allagan Tools制作窗口。")]
        public  void OpenCraftsWindow(string command, string args)
        {
            PluginService.WindowService.ToggleCraftsWindow();
        }

        [Command("/atconfig")]
        [HelpMessage("打开Allagan Tools配置窗口。")]
        public  void OpenConfigurationWindow(string command, string args)
        {
            PluginService.WindowService.ToggleConfigurationWindow();
        }

        [Command("/itconfig")]
        [DoNotShowInHelp]
        [HelpMessage("打开Allagan Tools配置窗口。")]
        public void OpenConfigurationWindowIT(string command, string args)
        {
            OpenConfigurationWindow(command, args);
        }

        [Command("/invconfig")]
        [DoNotShowInHelp]
        [HelpMessage("打开Allagan Tools配置窗口。")]
        public  void OpenConfigurationWindow2(string command, string args)
        {
            PluginService.WindowService.ToggleConfigurationWindow();
        }

        [Command("/athelp")]
        [HelpMessage("打开Allagan Tools帮助窗口。")]
        public  void OpenHelpWindow(string command, string args)
        {
            PluginService.WindowService.ToggleHelpWindow();
        }

        [Command("/invhelp")]
        [Aliases("/ithelp")]
        [DoNotShowInHelp]
        [HelpMessage("打开Allagan Tools帮助窗口。")]
        public  void OpenHelpWindow2(string command, string args)
        {
            PluginService.WindowService.ToggleHelpWindow();
        }
        
        #if DEBUG

        [Command("/atdebug")]
        [HelpMessage("打开Allagan Tools调试窗口。")]
        public  void ToggleDebugWindow(string command, string args)
        {
            PluginService.WindowService.ToggleDebugWindow();
        }

        [Command("/itdebug")]
        [DoNotShowInHelp]
        [HelpMessage("打开Allagan Tools调试窗口。")]
        public  void ToggleDebugWindowIT(string command, string args)
        {
            PluginService.WindowService.ToggleDebugWindow();
        }

        [Command("/atintro")]
        [HelpMessage("打开Allagan Tools调试窗口。")]
        public void ToggleIntroWindow(string command, string args)
        {
            PluginService.WindowService.OpenWindow<IntroWindow>(IntroWindow.AsKey);
        }

        [Command("/itintro")]
        [DoNotShowInHelp]
        [HelpMessage("打开Allagan Tools调试窗口。")]
        public void ToggleIntroWindowIT(string command, string args)
        {
            ToggleIntroWindow(command,args);
        }

        [Command("/atclearfilter")]
        [DoNotShowInHelp]
        [HelpMessage("Clears the active filter. Pass in background or ui to close just the background or ui filters respectively.")]
        public void ClearFilter(string command, string args)
        {
            args = args.Trim();
            if (args == "")
            {
                PluginService.FilterService.ClearActiveBackgroundFilter();
                PluginService.FilterService.ClearActiveUiFilter();
            }
            else if (args == "background")
            {
                PluginService.FilterService.ClearActiveBackgroundFilter();
            }
            else if (args == "ui")
            {
                PluginService.FilterService.ClearActiveUiFilter();
            }
        }

        [Command("/atclosefilters")]
        [DoNotShowInHelp]
        [HelpMessage("关闭所有过滤器窗口。")]
        public void CloseFilterWindows(string command, string args)
        {
            PluginService.WindowService.CloseFilterWindows();
        }

        [Command("/atclearall")]
        [DoNotShowInHelp]
        [HelpMessage("Closes all filter windows and clears all active filters. Pass in background or ui to close just the background or ui filters respectively.")]
        public void ClearAll(string command, string args)
        {
            ClearFilter(command, args);
            CloseFilterWindows(command,args);
        }

        #endif
    }
}