﻿using SpaceShooting.HUD;
using SpaceShooting.Manager;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceShooting.GameStates
{
	public class PauseState : GameState
	{
		Label[] lbls;
		Font fnt;
		Hud hud;

		public PauseState(GameStateManager gsm, Handler handler) : base(gsm, handler)
		{
			lbls = new Label[4];
			Color color = Color.White;
			fnt = new Font("Arial", 30f, FontStyle.Bold, GraphicsUnit.Pixel);
			int top = 250;

			lbls[0] = new Label()
			{
				Text = "PAUSE",
				Top = top - 150
			};

			lbls[1] = new Label()
			{
				Text = "BUY AMMUNICATION: 15",
				Top = top
			};

			lbls[2] = new Label()
			{
				Text = "BUY HEALTH: 20",
				Top = lbls[1].Top + 80
			};

			lbls[3] = new Label()
			{
				Text = "MAIN MENU",
				Top = lbls[2].Top + 80
			};

			foreach (Label lbl in lbls)
			{
				lbl.ForeColor = Color.White;
				lbl.BackColor = Color.Transparent;
				lbl.Font = fnt;
				lbl.TextAlign = ContentAlignment.MiddleCenter;
				lbl.Size = new Size(Game.WIDTH, Game.HEIGHT / 9);
				lbl.Left = (Game.WIDTH - lbl.Size.Width) / 2;
				lbl.Click += Click;
			}

			hud = new Hud();
		}

		private void Click(object sender, EventArgs e)
		{
			Label tempLabel = sender as Label;
			if (tempLabel == lbls[1])
			{
				if (Hud.COINS >= 15)
				{
					Hud.AMMO = 100;
					Hud.COINS -= 15;
				}
			}
			else if (tempLabel == lbls[2])
			{
				if (Hud.COINS >= 20)
				{
					Hud.COINS -= 20;
					Hud.HEALTH = 10;
				}
			}
			else if (tempLabel == lbls[3])
			{
				_gsm.Paused = false;
				Game.mainForm.Controls.Clear();
				Hud.COINS = 0;
				Hud.AMMO = 100;
				Hud.HEALTH = 10;
				_handler.entitiesList.Clear();
				_gsm.SetState(GameStateManager.MENU);
			}
		}

		public override void Update()
		{
			hud.Update();

			if (_gsm.Paused == true)
			{
				foreach (Label lbl in lbls)
				{
					Game.mainForm.Controls.Add(lbl);
				}
			}

			foreach (Label lbl in lbls)
			{

			}
		}

		public override void Render(Graphics g)
		{
			hud.Render(g);
		}

		public override void KeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				Game.mainForm.Controls.Clear();
				_gsm.Paused = false;
			}
		}

		public override void KeyUp(KeyEventArgs e)
		{
		}

		public override void MouseDown(MouseEventArgs e)
		{
		}

		public override void MouseUp(MouseEventArgs e)
		{
		}
	}
}
