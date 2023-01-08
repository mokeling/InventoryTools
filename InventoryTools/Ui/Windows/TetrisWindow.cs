using System.Numerics;
using ImGuiNET;
using InventoryTools.Logic;
using InventoryTools.Misc;
using Tetris.GameEngine;

namespace InventoryTools.Ui
{
    public class TetrisWindow : Window
    {
        public override bool SaveState => false;

        public TetrisWindow(string name = "Allagan Tools - 俄罗斯方块") : base(name)
        {
            
        }
        public TetrisWindow() : base("Allagan Tools - 俄罗斯方块")
        {
            
        }

        public override void Draw()
        {
            var tetrisGame = Misc.TetrisGame.Instance.Game;
            ImGui.TextWrapped("欢迎使用俄罗斯方块。");
            ImGui.Text("请打开俄罗斯方块覆盖，这将覆盖您的物品栏窗口内容。");
            ImGui.Text("请确保您的物品栏显示类型设置为“全展开”。当叠加层处于活动状态时，您将无法访问您的物品栏。");

            if (ImGui.Button(TetrisGame.TetrisEnabled ? "禁用俄罗斯方块覆盖" : "启用俄罗斯方块叠加"))
            {
                TetrisGame.ToggleTetris();
            }
            
            ImGui.Text("覆盖：" + (TetrisGame.TetrisEnabled ? "启用" : "禁用"));
            ImGui.Text("当前状态：" + tetrisGame.Status.ToString());
            if ((tetrisGame.Status == Game.GameStatus.ReadyToStart || tetrisGame.Status == Game.GameStatus.Finished) && ImGui.Button("开始"))
            {
                if (tetrisGame.Status == Game.GameStatus.Finished)
                {
                    TetrisGame.Restart();
                }
                else
                {
                    tetrisGame.Start();
                }
            }
            if (tetrisGame.Status == Game.GameStatus.InProgress && ImGui.Button("暂停"))
            {
                tetrisGame.Pause();
            }
            if ((tetrisGame.Status == Game.GameStatus.InProgress || tetrisGame.Status == Game.GameStatus.Paused) && ImGui.Button("游戏结束"))
            {
                tetrisGame.GameOver();
            }
        }

        public override Vector2 DefaultSize { get; } = new Vector2(800, 300);
        public override Vector2 MaxSize => new Vector2(5000, 5000);
        public override Vector2 MinSize => new Vector2(300, 300);

        public override void Invalidate()
        {
            
        }
        public override FilterConfiguration? SelectedConfiguration => null;

        public static string AsKey => "tetris";
        public override string Key => AsKey;
        public override bool DestroyOnClose => true;
    }
}