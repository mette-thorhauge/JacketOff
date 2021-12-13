﻿using ConsumerDesktopClient.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumerDesktopClient.Gui {
    public partial class ModtagAngivAntal : UserControl {

        private OrderController orderController;
        private int antalJakker;
        private int antalTasker;

        public ModtagAngivAntal() {
            orderController = OrderController.GetInstance();
            InitializeComponent();
        }

        private void buttonAfbryd_Click(object sender, EventArgs e) {
          
            //We bring the start user panel back to the front
            Start.GetInstance().PnlModtag.Controls["ModtagStart"].BringToFront();
        }

        private void buttonNaeste_Click(object sender, EventArgs e) {
            
            //We check whether our Start form does no contain an instantiated version
            //of our user controller, we create one and add it to our Start form
            if (!Start.GetInstance().PnlModtag.Controls.ContainsKey("ModtagKvittering")) {
                
                ModtagKvittering ucModtagKvittering = new ModtagKvittering();
                ucModtagKvittering.Dock = DockStyle.Fill;
                Start.GetInstance().PnlModtag.Controls.Add(ucModtagKvittering);
            }

            orderController.AntalTasker = antalTasker;
            orderController.AntalJakker = antalJakker;

            //We bring the next user controller to the front of our panel
            Start.GetInstance().PnlModtag.Controls["ModtagKvittering"].BringToFront();
        }

        private void buttonPlusJakker_Click(object sender, EventArgs e) {
            antalJakker = Convert.ToInt32(textBoxJakkeNumre.Text) + 1;
            textBoxJakkeNumre.Text = (antalJakker).ToString();
        }

        private void buttonMinusJakker_Click(object sender, EventArgs e) {
            antalJakker = Convert.ToInt32(textBoxJakkeNumre.Text);

            if (antalJakker > 0) {
                antalJakker = antalJakker - 1;
                textBoxJakkeNumre.Text = antalJakker.ToString();
            } 
        }

        private void buttonPlusTasker_Click(object sender, EventArgs e) {
            antalTasker = Convert.ToInt32(textBoxTaskeNumre.Text) + 1;
            textBoxTaskeNumre.Text = (antalTasker).ToString();
        }

        private void buttonMinusTasker_Click(object sender, EventArgs e) {
            antalTasker = Convert.ToInt32(textBoxTaskeNumre.Text);

            if (antalTasker > 0) {
                antalTasker = antalTasker - 1;
                textBoxTaskeNumre.Text = (antalTasker).ToString();
            }
        }
    }
}
