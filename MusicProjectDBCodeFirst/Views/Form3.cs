using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicProjectDBCodeFirst.Controller;
using MusicProjectDBCodeFirst.Data;
using MusicProjectDBCodeFirst.Data.Models;
namespace MusicProjectDBCodeFirst.Views
{
    public partial class Form3 : Form
    {
        AlbumController albumController;
        //InstrumentController instrumentController;
        PerformerController performerController;
        //SongController songController;
        //PerformerInstrumentController performerInstrumentController;
        RecordLabelController recordLabelController;

        public Form3()
        {
            InitializeComponent();
            albumController = new AlbumController();
            //instrumentController = new InstrumentController();
            performerController = new PerformerController();
            //songController = new SongController();
            //performerInstrumentController = new PerformerInstrumentController();
            recordLabelController = new RecordLabelController();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Performer performer = performerController.GetPerformerByName(textBox1.Text);
                if (performer == null)
                    throw new MyException("This performer doesn't exist");

                RecordLabel recordLabel = recordLabelController.GetLabelByName(textBox2.Text);
                if (recordLabel == null)
                    throw new MyException("This record label doesn't exist");

                performerController.UpdateLabel(performer, recordLabel);
                performer.RecordLabel = recordLabel;

                textBox1.Clear();
                textBox2.Clear();
            }
            catch(MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var album = albumController.GetAlbumByName(textBox3.Text);

                if (album == null)
                    throw new MyException("This album doesn't exist");

                albumController.ChangeAlbumName(album, textBox4.Text);

                textBox3.Clear();
                textBox4.Clear();
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Performer performer = performerController.GetPerformerByName(textBox5.Text);
                if (performer == null)
                    throw new MyException("This performer doesn't exist");

                performerController.ChangeName(performer, textBox6.Text);

                textBox5.Clear();
                textBox6.Clear();
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
