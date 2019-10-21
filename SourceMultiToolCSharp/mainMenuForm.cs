using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SourceMultiToolCSharp
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        public static Steam Steam = new Steam();
        public static List<SourceGame> SourceGames = new List<SourceGame>();
        public static string Version = "3.0.0";

        private void MainMenuFormLoad(object sender, EventArgs e)
        {
            versionLabel.Text += Version;

            // Add Source Games
            SourceGames.Add(new SourceGame
            {
                SteamName = "Alien Swarm",
                ProperName = "Alien Swarm",
                SourceName = "swarm",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Contagion",
                ProperName = "Contagion",
                SourceName = "contagion",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Counter-Strike Global Offensive",
                ProperName = "Counter-Strike Global Offensive",
                SourceName = "csgo",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Counter-Strike Source",
                ProperName = "Counter-Strike Source",
                SourceName = "cstrike",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Day of Defeat Source",
                ProperName = "Day of Defeat Source",
                SourceName = "dod",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "dayofinfamy",
                ProperName = "Day of Infamy",
                SourceName = "doi",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Dino D-Day",
                ProperName = "Dino D-Day",
                SourceName = "dinodday",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Double Action",
                ProperName = "Double Action",
                SourceName = "dab",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Empires",
                ProperName = "Empires",
                SourceName = "empires",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "GarrysMod",
                ProperName = "Garry's Mod",
                SourceName = "garrysmod",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Half-Life 1 Source Deathmatch",
                ProperName = "Half-Life Source Deathmatch",
                SourceName = "hl1mp",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Half-Life 2",
                ProperName = "Half-Life 2",
                SourceName = "hl2",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Half-Life 2 Deathmatch",
                ProperName = "Half-Life 2 Deathmatch",
                SourceName = "hl2mp",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "insurgency2",
                ProperName = "Insurgency",
                SourceName = "insurgency",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Left 4 Dead 2",
                ProperName = "Left 4 Dead 2",
                SourceName = "left4dead2",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "nmrih",
                ProperName = "No More Room In Hell",
                SourceName = "nmrih",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Nuclear Dawn",
                ProperName = "Nuclear Dawn",
                SourceName = "nucleardawn",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "pirates, vikings and knights ii",
                ProperName = "Pirates, Vikings, and Knights II",
                SourceName = "pvkii",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Portal",
                ProperName = "Portal",
                SourceName = "portal",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Portal 2",
                ProperName = "Portal 2",
                SourceName = "portal2",
                Installed = false,
                Directory = ""
            });
            SourceGames.Add(new SourceGame
            {
                SteamName = "Team Fortress 2",
                ProperName = "Team Fortress 2",
                SourceName = "tf",
                Installed = false,
                Directory = ""
            });

            // Load in saved properties if still valid
            if (File.Exists(Properties.Settings.Default.mainSteamDir + "/Steam.exe"))
            {
                Steam.MainSteamDir = Properties.Settings.Default.mainSteamDir;
                textSteamDirectory.Text = Properties.Settings.Default.mainSteamDir;
                FindSteamDirectories();
            }
        }

        private void FindSteamDirectories()
        {
			//TODO: Surely there is a library for reading .vdf files

            if (File.Exists(Steam.MainSteamDir + "/steamapps/libraryfolders.vdf"))
            {
				//If the libraryfolders.vdf file exists, then read all the lines
                string[] lines = File.ReadAllLines(Steam.MainSteamDir + "/steamapps/libraryfolders.vdf");

                if (lines.Length != 5)
                {
                    //This means we have multiple directories
                    Steam.AdditionalSteamDirectories.Clear();

                    for (int i = 4; i < lines.Length - 1; i++)    //start at line 5 and go to closing bracket
                    {
                        string temp = lines[i];
                        int finalPosition = temp.LastIndexOf("\"", StringComparison.Ordinal);
                        int startPosition = temp.LastIndexOf("\"", finalPosition - 1, StringComparison.Ordinal) + 1;    //Don't grab the same position or starting quote

                        temp = temp.Substring(startPosition, (finalPosition - startPosition)).Replace("\\\\", "\\");
                        Steam.AdditionalSteamDirectories.Add(temp);
                    }
                    richTextBoxAdditionalSteamDirectory.Lines = Steam.AdditionalSteamDirectories.ToArray();
                }
            }

            CheckGamesInstalled();
        }

        private void CheckGamesInstalled()
        {
            foreach (SourceGame game in SourceGames.Where(game => Directory.Exists(Steam.MainSteamDir + "\\steamapps\\common\\" + game.SteamName)))
            {
	            game.Directory = Steam.MainSteamDir + "\\steamapps\\common\\" + game.SteamName;
	            game.Installed = true;
            }
            // Cycle through all additional directories
            foreach (string dir in Steam.AdditionalSteamDirectories)
            {
	            foreach (SourceGame game in SourceGames.Where(game => Directory.Exists(dir + "\\steamapps\\common\\" + game.SteamName)))
	            {
		            game.Directory = dir + "\\steamapps\\common\\" + game.SteamName;
		            game.Installed = true;
	            }
            }
            UpdateGameDropDown();
            UpdateDebugInfo();
        }

        private void UpdateDebugInfo()
        {
            StringBuilder sb = new StringBuilder();
            foreach (SourceGame game in SourceGames)
            {
                sb.Append("SteamName: ");
                sb.AppendLine(game.SteamName);
                sb.Append("ProperName: ");
                sb.AppendLine(game.ProperName);
                sb.Append("SourceName: ");
                sb.AppendLine(game.SourceName);
                sb.Append("Installed: ");
                sb.AppendLine(game.Installed.ToString());
                if (game.Installed)
                {
                    sb.Append("Directory: ");
                    sb.AppendLine(game.Directory);
                }
                sb.AppendLine("\n");
            }
            richTextBoxSourceGameDebug.Text = sb.ToString();
        }

        private void UpdateGameDropDown()
        {
            // Clear the combobox and disable buttons
            comboBoxGames.Items.Clear();
            hammer_btn.Enabled = false;
            modelViewer_btn.Enabled = false;
            button_gmodConfig.Enabled = false;

            foreach (SourceGame game in SourceGames.Where(game => game.Installed))
            {
	            comboBoxGames.Items.Add(game.ProperName);
            }
            if (comboBoxGames.Items.Count > 0)
            {
                comboBoxGames.SelectedIndex = 0;
                // Reenable
                hammer_btn.Enabled = true;
                modelViewer_btn.Enabled = true;
            }
            if(comboBoxGames.Items.Contains("Garry's Mod"))
            {
                button_gmodConfig.Enabled = true;
            }
        }

		#region Button Events

		private void HammerButtonClick(object sender, EventArgs e)
        {
            string gameName = comboBoxGames.GetItemText(comboBoxGames.SelectedItem);
            string directory = SourceGames.First(item => item.ProperName == gameName).Directory;
            if (!File.Exists(directory + "\\bin\\hammer.exe"))
            {
                switch (gameName)
                {
                    case "Alien Swarm":
                    case "Counter-Strike Global Offensive":
                    case "Dino D-Day":
                    case "Left 4 Dead 2":
                    case "Nuclear Dawn":
                    case "Portal 2":
                        MessageBox.Show(
	                        $"No Hammer install could be found at {directory}\\bin\\hammer.exe \n\n Make sure you have installed it's authoring tools in Steam!", "ERROR 002", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    case "No More Room In Hell":
                        if (!File.Exists(directory + "\\sdk\\bin\\hammer.exe"))
                        {
                            MessageBox.Show($"No Hammer install could be found at {directory}\\sdk\\bin\\hammer.exe", "ERROR 002", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    case "Pirates, Vikings, and Knights II":
                        if (!File.Exists(directory + "\\sdkbase_pvkii\\bin\\hammer.exe"))
                        {
                            MessageBox.Show(
	                            $"No Hammer install could be found at {directory}\\sdkbase_pvkii\\bin\\hammer.exe", "ERROR 002", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    default:
                        MessageBox.Show($"No Hammer install could be found at {directory}\\bin\\hammer.exe", "ERROR 002", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

            }
#pragma warning disable IDE0067 // Dispose objects before losing scope
	        // ReSharper disable once UseObjectOrCollectionInitializer
	        Process hammer = new Process();
#pragma warning restore IDE0067 // Dispose objects before losing scope
			hammer.StartInfo.FileName = "CMD.exe";
            if (gameName == "No More Room In Hell")
                hammer.StartInfo.Arguments = "/c cd /d " + directory + "\\sdk\\bin && start \"\" hammer.exe -nop4";
            else if (gameName == "Pirates, Vikings, and Knights II")
                hammer.StartInfo.Arguments = "/c cd /d " + directory + "\\sdkbase_pvkii\\bin && start \"\" hammer.exe -nop4";
            else
                hammer.StartInfo.Arguments = "/c cd /d " + directory + "\\bin && start \"\" hammer.exe -nop4";
            hammer.Start();
        }

        private void ModelViewerButtonClick(object sender, EventArgs e)
        {
            string gameName = comboBoxGames.GetItemText(comboBoxGames.SelectedItem);
            string directory = SourceGames.First(item => item.ProperName == gameName).Directory;
            string sourcename = SourceGames.First(item => item.ProperName == gameName).SourceName;
            if (!File.Exists(directory + "\\bin\\hlmv.exe"))
            {
                switch (gameName)
                {
                    case "Alien Swarm":
                    case "Counter-Strike Global Offensive":
                    case "Dino D-Day":
                    case "Left 4 Dead 2":
                    case "Nuclear Dawn":
                    case "Portal 2":
                        MessageBox.Show(String.Format("No Hammer install could be found at {0}\\bin\\hlmv.exe \n\n Make sure you have installed it's authoring tools in Steam!", directory), "ERROR 003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    case "No More Room In Hell":
                        if (!File.Exists(directory + "\\sdk\\bin\\hlmv.exe"))
                        {
                            MessageBox.Show(String.Format("No Hammer install could be found at {0}\\sdk\\bin\\hlmv.exe", directory), "ERROR 003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    case "Pirates, Vikings, and Knights II":
                        if (!File.Exists(directory + "\\sdkbase_pvkii\\bin\\hlmv.exe"))
                        {
                            MessageBox.Show(String.Format("No Hammer install could be found at {0}\\sdkbase_pvkii\\bin\\hlmv.exe", directory), "ERROR 003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    default:
                        MessageBox.Show(String.Format("No Hammer install could be found at {0}\\bin\\hlmv.exe", directory), "ERROR 003", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }
            }

			// ReSharper disable once UseObjectOrCollectionInitializer
#pragma warning disable IDE0067 // Dispose objects before losing scope
			Process modelviewer = new Process();
#pragma warning restore IDE0067 // Dispose objects before losing scope
			modelviewer.StartInfo.FileName = "CMD.exe";
            if (gameName == "No More Room In Hell")
                modelviewer.StartInfo.Arguments = "/c cd /d " + directory + "\\sdk\\bin && start \"\" hlmv.exe -game \"" + directory + "\\" + sourcename + "\"";
            else if (gameName == "Pirates, Vikings, and Knights II")
                modelviewer.StartInfo.Arguments = "/c cd /d " + directory + "\\sdkbase_pvkii\\bin && start \"\" hlmv.exe -game \"" + directory + "\\" + sourcename + "\"";
            else
                modelviewer.StartInfo.Arguments = "/c cd /d " + directory + "\\bin && start \"\" hlmv.exe -game \""+directory+"\\"+sourcename+"\"";
            modelviewer.Start();
        }

        private void GmodConfigButtonClick(object sender, EventArgs e)
        {
#pragma warning disable IDE0067 // Dispose objects before losing scope
			GarrysModConfigForm gmodConfigForm = new GarrysModConfigForm();
#pragma warning restore IDE0067 // Dispose objects before losing scope

			gmodConfigForm.ShowDialog();
        }

        private void ResetSettingsButtonClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to reset settings back to default?", "WARNING", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            Properties.Settings.Default.Reset();
            hammer_btn.Enabled = false;
            modelViewer_btn.Enabled = false;
            textSteamDirectory.Clear();
            richTextBoxAdditionalSteamDirectory.Clear();
            richTextBoxSourceGameDebug.Clear();
            button_gmodConfig.Enabled = false;
            foreach (SourceGame game in SourceGames)
            {
	            game.Directory = "";
	            game.Installed = false;
            }
            comboBoxGames.Items.Clear();
        }

        private void BrowseSteamDirButtonClick(object sender, EventArgs e)
        {
	        DialogResult result = folderBrowserDialogSteam.ShowDialog();
	        if (result == DialogResult.OK)
	        {
		        textSteamDirectory.Text = folderBrowserDialogSteam.SelectedPath;
	        }
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
	        if (File.Exists(textSteamDirectory.Text + "/Steam.exe"))
	        {
		        Properties.Settings.Default.mainSteamDir = textSteamDirectory.Text;
		        Properties.Settings.Default.Save();
		        Steam.MainSteamDir = Properties.Settings.Default.mainSteamDir;

		        MessageBox.Show("Save Complete!", "Saved", MessageBoxButtons.OK);

		        FindSteamDirectories();
	        }
	        else
	        {
		        MessageBox.Show("The Main Steam Directory you have chosen does not contain Steam.exe meaning it is not your main directory", "ERROR 001", MessageBoxButtons.OK, MessageBoxIcon.Warning);
	        }
        }

		#endregion
    }
}
