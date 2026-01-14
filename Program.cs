using System.Drawing.Drawing2D;

namespace Pongis_9000
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MenuForm());
        }
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public class MenuForm : Form
    {
        private const int WindowWidth = 900;
        private const int WindowHeight = 520;
        private Difficulty selectedDifficulty = Difficulty.Medium;
        private Button selectedDifficultyBtn;

        public MenuForm()
        {
            ClientSize = new Size(WindowWidth, WindowHeight);
            Text = "Pongis 9000 .NET  - Main Menu";
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.Black;
            DoubleBuffered = true;

            Label diffLabel = new()
            {
                Text = "SELECT AI DIFFICULTY",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 200, 255),
                Size = new Size(900, 30),
                Location = new Point(0, 100),
                TextAlign = ContentAlignment.MiddleCenter
            };

            int buttonWidth = 120;
            int buttonSpacing = 15;
            int totalWidth = buttonWidth * 3 + buttonSpacing * 2;
            int startX = (WindowWidth - totalWidth) / 2;

            Button easyBtn = CreateDifficultyButton("Easy", startX, 150, buttonWidth);
            easyBtn.Click += (s, e) => SetDifficulty(Difficulty.Easy, easyBtn);

            Button mediumBtn = CreateDifficultyButton("Medium", startX + buttonWidth + buttonSpacing, 150, buttonWidth);
            mediumBtn.Click += (s, e) => SetDifficulty(Difficulty.Medium, mediumBtn);

            Button hardBtn = CreateDifficultyButton("Hard", startX + (buttonWidth + buttonSpacing) * 2, 150, buttonWidth);
            hardBtn.Click += (s, e) => SetDifficulty(Difficulty.Hard, hardBtn);

            selectedDifficultyBtn = mediumBtn;
            HighlightDifficultyButton(mediumBtn);

            Label gameModeLabel = new()
            {
                Text = "GAME MODE",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 200, 255),
                Size = new Size(900, 30),
                Location = new Point(0, 240),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button singlePlayerBtn = new()
            {
                Text = "Single Player",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Size = new Size(280, 70),
                Location = new Point((WindowWidth - 280) / 2, 290),
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            singlePlayerBtn.FlatAppearance.BorderColor = Color.FromArgb(100, 200, 255);
            singlePlayerBtn.FlatAppearance.BorderSize = 2;
            singlePlayerBtn.MouseEnter += (s, e) => HoverButton(singlePlayerBtn, true);
            singlePlayerBtn.MouseLeave += (s, e) => HoverButton(singlePlayerBtn, false);
            singlePlayerBtn.Click += (s, e) => StartGame(true);

            Button twoPlayerBtn = new()
            {
                Text = "Two Players",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Size = new Size(280, 70),
                Location = new Point((WindowWidth - 280) / 2, 375),
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            twoPlayerBtn.FlatAppearance.BorderColor = Color.FromArgb(100, 200, 255);
            twoPlayerBtn.FlatAppearance.BorderSize = 2;
            twoPlayerBtn.MouseEnter += (s, e) => HoverButton(twoPlayerBtn, true);
            twoPlayerBtn.MouseLeave += (s, e) => HoverButton(twoPlayerBtn, false);
            twoPlayerBtn.Click += (s, e) => StartGame(false);

            Button exitBtn = new()
            {
                Text = "Exit",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Size = new Size(150, 50),
                Location = new Point((WindowWidth - 150) / 2, 460),
                BackColor = Color.FromArgb(60, 30, 30),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            exitBtn.FlatAppearance.BorderColor = Color.FromArgb(180, 80, 80);
            exitBtn.FlatAppearance.BorderSize = 2;
            exitBtn.MouseEnter += (s, e) => HoverButton(exitBtn, true);
            exitBtn.MouseLeave += (s, e) => HoverButton(exitBtn, false);
            exitBtn.Click += (s, e) => Close();

            Controls.Add(diffLabel);
            Controls.Add(easyBtn);
            Controls.Add(mediumBtn);
            Controls.Add(hardBtn);
            Controls.Add(gameModeLabel);
            Controls.Add(singlePlayerBtn);
            Controls.Add(twoPlayerBtn);
            Controls.Add(exitBtn);

            Paint += OnPaint;
        }

        private static Button CreateDifficultyButton(string text, int x, int y, int width)
        {
            Button btn = new()
            {
                Text = text,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Size = new Size(width, 50),
                Location = new Point(x, y),
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
            btn.FlatAppearance.BorderSize = 2;
            return btn;
        }

        private static void HighlightDifficultyButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(0, 150, 200);
            btn.FlatAppearance.BorderColor = Color.Cyan;
            btn.FlatAppearance.BorderSize = 3;
            btn.Font = new Font("Segoe UI", 13, FontStyle.Bold | FontStyle.Underline);
        }

        private static void UnhighlightDifficultyButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(50, 50, 50);
            btn.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
            btn.FlatAppearance.BorderSize = 2;
            btn.Font = new Font("Segoe UI", 13, FontStyle.Bold);
        }

        private void SetDifficulty(Difficulty diff, Button clickedBtn)
        {
            selectedDifficulty = diff;
            if (selectedDifficultyBtn != null)
            {
                UnhighlightDifficultyButton(selectedDifficultyBtn);
            }
            HighlightDifficultyButton(clickedBtn);
            selectedDifficultyBtn = clickedBtn;
        }

        private void HoverButton(Button btn, bool isHovering)
        {

            bool isDifficultyButton = (btn == selectedDifficultyBtn) ||
                                     (btn.Text == "Easy" || btn.Text == "Medium" || btn.Text == "Hard");

            if (isHovering)
            {
                btn.BackColor = Color.FromArgb(100, 100, 100);
                btn.FlatAppearance.BorderColor = Color.Cyan;
                btn.FlatAppearance.BorderSize = 4;
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size, FontStyle.Bold | FontStyle.Underline);
            }
            else
            {
                if (btn == selectedDifficultyBtn)
                {
                    HighlightDifficultyButton(btn);
                }
                else if (isDifficultyButton)
                {
                    UnhighlightDifficultyButton(btn);
                }
                else if (btn.Text == "Exit")
                {
                    btn.BackColor = Color.FromArgb(60, 30, 30);
                    btn.FlatAppearance.BorderColor = Color.FromArgb(180, 80, 80);
                    btn.FlatAppearance.BorderSize = 2;
                    btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size, FontStyle.Bold);
                }
                else
                {
                    btn.BackColor = Color.FromArgb(50, 50, 50);
                    btn.FlatAppearance.BorderColor = Color.FromArgb(100, 200, 255);
                    btn.FlatAppearance.BorderSize = 2;
                    btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size, FontStyle.Bold);
                }
            }
        }

        private void StartGame(bool singlePlayer)
        {
            Hide();
            GameForm game = new(singlePlayer, selectedDifficulty);
            game.FormClosed += (s, e) => { Show(); };
            game.Show();
        }

        private void OnPaint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            using Font titleFont = new("Consolas", 72, FontStyle.Bold);
            using Brush gradientBrush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(ClientSize.Width, 0),
                Color.FromArgb(100, 200, 255),
                Color.FromArgb(0, 150, 200)
            );
            string title = "PONGIS";
            SizeF titleSize = g.MeasureString(title, titleFont);
            g.DrawString(title, titleFont, gradientBrush, (ClientSize.Width - titleSize.Width) / 2, 15);
        }
    }

    public class GameForm : Form
    {
        private const int WindowWidth = 900;
        private const int WindowHeight = 520;
        private const int PaddleWidth = 12;
        private const int PaddleHeight = 100;
        private const int BallSize = 14;
        private const int PaddleSpeed = 7;
        private const float InitialBallSpeed = 6f;
        private const int WinningScore = 5;

        private float aiDifficulty;
        private int aiReactionDelay;
        private int aiDelayCounter = 0;

        private Rectangle leftPaddle;
        private Rectangle rightPaddle;
        private RectangleF ball;
        private PointF ballVelocity;
        private int leftScore;
        private int rightScore;
        private readonly System.Windows.Forms.Timer gameTimer;
        private bool wDown;
        private bool sDown;
        private bool upDown;
        private bool downDown;
        private readonly Random rnd = new();
        private readonly bool isSinglePlayer;
        private readonly Difficulty difficulty;
        private bool gameOver = false;
        private string winner = "";

        public GameForm(bool singlePlayer, Difficulty diff)
        {
            isSinglePlayer = singlePlayer;
            difficulty = diff;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    aiDifficulty = 0.25f;
                    aiReactionDelay = 20;
                    break;
                case Difficulty.Medium:
                    aiDifficulty = 0.50f;
                    aiReactionDelay = 12;
                    break;
                case Difficulty.Hard:
                    aiDifficulty = 0.75f;
                    aiReactionDelay = 6;
                    break;
            }

            ClientSize = new Size(WindowWidth, WindowHeight);
            string diffText = singlePlayer ? $" - {difficulty}" : "";
            Text = singlePlayer ? $"Pong - Single Player{diffText}" : "Pong - Two Players";
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.Black;
            DoubleBuffered = true;

            leftPaddle = new Rectangle(30, (WindowHeight - PaddleHeight) / 2, PaddleWidth, PaddleHeight);
            rightPaddle = new Rectangle(ClientSize.Width - 30 - PaddleWidth, (WindowHeight - PaddleHeight) / 2, PaddleWidth, PaddleHeight);
            ball = new RectangleF((ClientSize.Width - BallSize) / 2f, (ClientSize.Height - BallSize) / 2f, BallSize, BallSize);
            ResetBall(rnd.Next(2) == 0);

            gameTimer = new System.Windows.Forms.Timer { Interval = 16 };
            gameTimer.Tick += GameTick;
            gameTimer.Start();

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
            Paint += OnPaint;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void ResetBall(bool toRight)
        {
            ball.X = (ClientSize.Width - BallSize) / 2f;
            ball.Y = (ClientSize.Height - BallSize) / 2f;

            double angle = rnd.NextDouble() * 0.9 - 0.45;
            float vx = (float)(InitialBallSpeed * (toRight ? 1 : -1) * Math.Cos(angle));
            float vy = (float)(InitialBallSpeed * Math.Sin(angle));
            ballVelocity = new PointF(vx, vy);
            aiDelayCounter = 0;
        }

        private void GameTick(object? sender, EventArgs e)
        {
            if (gameOver) return;

            MovePaddles();

            if (isSinglePlayer)
            {
                MoveAIPaddle();
            }

            ball.X += ballVelocity.X;
            ball.Y += ballVelocity.Y;

            if (ball.Top <= 0)
            {
                ball.Y = 0;
                ballVelocity.Y = -ballVelocity.Y;
            }
            else if (ball.Bottom >= ClientSize.Height)
            {
                ball.Y = ClientSize.Height - BallSize;
                ballVelocity.Y = -ballVelocity.Y;
            }

            if (Intersects(ball, leftPaddle))
            {
                ball.X = leftPaddle.Right;
                BounceOffPaddle(leftPaddle);
            }
            else if (Intersects(ball, rightPaddle))
            {
                ball.X = rightPaddle.Left - BallSize;
                BounceOffPaddle(rightPaddle);
            }

            if (ball.Right < 0)
            {
                rightScore++;
                CheckWinCondition();
                if (!gameOver) ResetBall(true);
            }
            else if (ball.Left > ClientSize.Width)
            {
                leftScore++;
                CheckWinCondition();
                if (!gameOver) ResetBall(false);
            }

            Invalidate();
        }

        private void CheckWinCondition()
        {
            if (leftScore >= WinningScore)
            {
                gameOver = true;
                winner = isSinglePlayer ? "You Win!" : "Left Player Wins!";
            }
            else if (rightScore >= WinningScore)
            {
                gameOver = true;
                winner = isSinglePlayer ? "AI Wins!" : "Right Player Wins!";
            }
        }

        private void MoveAIPaddle()
        {
            aiDelayCounter++;
            if (aiDelayCounter < aiReactionDelay) return;

            if (ballVelocity.X <= 0) return;

            float paddleCenter = rightPaddle.Y + rightPaddle.Height / 2f;
            float ballCenter = ball.Y + ball.Height / 2f;

            float targetY = ballCenter;

            float errorRange = difficulty == Difficulty.Easy ? 150f :
                              difficulty == Difficulty.Medium ? 100f : 60f;

            if (rnd.NextDouble() > aiDifficulty)
            {
                targetY += (float)(rnd.NextDouble() * errorRange * 2 - errorRange);
            }

            if (rnd.NextDouble() > aiDifficulty + 0.1)
            {
                return;
            }

            float diff = targetY - paddleCenter;

            if (Math.Abs(diff) > 30)
            {
                if (diff < 0)
                {
                    rightPaddle.Y = Math.Max(0, rightPaddle.Y - PaddleSpeed);
                }
                else
                {
                    rightPaddle.Y = Math.Min(ClientSize.Height - PaddleHeight, rightPaddle.Y + PaddleSpeed);
                }
            }
        }

        private void MovePaddles()
        {
            if (wDown) leftPaddle.Y = Math.Max(0, leftPaddle.Y - PaddleSpeed);
            if (sDown) leftPaddle.Y = Math.Min(ClientSize.Height - PaddleHeight, leftPaddle.Y + PaddleSpeed);

            if (!isSinglePlayer)
            {
                if (upDown) rightPaddle.Y = Math.Max(0, rightPaddle.Y - PaddleSpeed);
                if (downDown) rightPaddle.Y = Math.Min(ClientSize.Height - PaddleHeight, rightPaddle.Y + PaddleSpeed);
            }
        }

        private static bool Intersects(RectangleF r, Rectangle padd)
        {
            return !(r.Right < padd.Left || r.Left > padd.Right || r.Bottom < padd.Top || r.Top > padd.Bottom);
        }

        private void BounceOffPaddle(Rectangle paddle)
        {
            float paddleCenter = paddle.Y + paddle.Height / 2f;
            float ballCenter = ball.Y + ball.Height / 2f;
            float relative = (ballCenter - paddleCenter) / (paddle.Height / 2f);
            relative = Math.Max(-1f, Math.Min(1f, relative));

            const double maxBounce = Math.PI * 65.0 / 180.0;
            double bounceAngle = relative * maxBounce;

            float speed = (float)Math.Sqrt(ballVelocity.X * ballVelocity.X + ballVelocity.Y * ballVelocity.Y);
            speed = Math.Min(70f, speed * 1.08f);

            int direction = paddle == leftPaddle ? 1 : -1;
            ballVelocity.X = (float)(direction * speed * Math.Cos(bounceAngle));
            ballVelocity.Y = (float)(speed * Math.Sin(bounceAngle));
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                if (e.KeyCode == Keys.R)
                {
                    leftScore = 0;
                    rightScore = 0;
                    gameOver = false;
                    winner = "";
                    ResetBall(rnd.Next(2) == 0);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    Close();
                }
                return;
            }

            if (e.KeyCode == Keys.W) wDown = true;
            else if (e.KeyCode == Keys.S) sDown = true;
            else if (e.KeyCode == Keys.Up && !isSinglePlayer) upDown = true;
            else if (e.KeyCode == Keys.Down && !isSinglePlayer) downDown = true;
            else if (e.KeyCode == Keys.R)
            {
                leftScore = 0;
                rightScore = 0;
                ResetBall(rnd.Next(2) == 0);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void OnKeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) wDown = false;
            else if (e.KeyCode == Keys.S) sDown = false;
            else if (e.KeyCode == Keys.Up) upDown = false;
            else if (e.KeyCode == Keys.Down) downDown = false;
        }

        private void OnPaint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            using Pen dash = new(Color.White, 2) { DashPattern = [8, 10] };
            g.DrawLine(dash, ClientSize.Width / 2, 0, ClientSize.Width / 2, ClientSize.Height);

            using Brush white = new SolidBrush(Color.White);
            g.FillRectangle(white, leftPaddle);
            g.FillRectangle(white, rightPaddle);
            g.FillEllipse(white, ball);

            using Font scoreFont = new("Consolas", 34, FontStyle.Bold);
            string leftText = leftScore.ToString();
            string rightText = rightScore.ToString();
            SizeF ls = g.MeasureString(leftText, scoreFont);
            SizeF rs = g.MeasureString(rightText, scoreFont);
            g.DrawString(leftText, scoreFont, white, ClientSize.Width / 2f - 90f - ls.Width / 2f, 18f);
            g.DrawString(rightText, scoreFont, white, ClientSize.Width / 2f + 90f - rs.Width / 2f, 18f);

            if (gameOver)
            {
                using Brush overlay = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
                g.FillRectangle(overlay, 0, 0, ClientSize.Width, ClientSize.Height);
                using Font victoryFont = new("Consolas", 60, FontStyle.Bold);
                SizeF victorySize = g.MeasureString(winner, victoryFont);
                float victoryX = (ClientSize.Width - victorySize.Width) / 2;
                float victoryY = (ClientSize.Height - victorySize.Height) / 2 - 40;
                using Brush shadow = new SolidBrush(Color.Black);
                g.DrawString(winner, victoryFont, shadow, victoryX + 3, victoryY + 3);
                using Brush gold = new SolidBrush(Color.Gold);
                g.DrawString(winner, victoryFont, gold, victoryX, victoryY);

                using Font finalScoreFont = new("Segoe UI", 24, FontStyle.Bold);
                string finalScore = $"Final Score: {leftScore} - {rightScore}";
                SizeF finalScoreSize = g.MeasureString(finalScore, finalScoreFont);
                float finalScoreX = (ClientSize.Width - finalScoreSize.Width) / 2;
                g.DrawString(finalScore, finalScoreFont, white, finalScoreX, victoryY + 80);

                using Font instructFont = new("Segoe UI", 16);
                string instruct = "Press R to Play Again  |  Esc for Main Menu";
                SizeF instructSize = g.MeasureString(instruct, instructFont);
                float instructX = (ClientSize.Width - instructSize.Width) / 2;
                using Brush instructBrush = new SolidBrush(Color.FromArgb(220, Color.White));
                g.DrawString(instruct, instructFont, instructBrush, instructX, victoryY + 140);
            }
            else
            {
                using Font hint = new("Segoe UI", 9);
                using Brush hintBrush = new SolidBrush(Color.FromArgb(190, Color.White));
                string controls = isSinglePlayer
                    ? "W S = Move paddle    R = Reset    Esc = Main Menu"
                    : "W S = Left paddle    Up Down = Right paddle    R = Reset    Esc = Main Menu";
                g.DrawString(controls, hint, hintBrush, 8, ClientSize.Height - 20);
            }
        }
    }
}