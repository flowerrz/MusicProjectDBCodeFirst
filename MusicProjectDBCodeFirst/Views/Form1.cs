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

namespace MusicProjectDBCodeFirst
{
    public partial class Form1 : Form
    {
        static byte[] image;
        AlbumController albumController;
        InstrumentController instrumentController;
        PerformerController performerController;
        SongController songController;
        PerformerInstrumentController performerInstrumentController;
        RecordLabelController recordLabelController;

        List<Instrument> instruments = new List<Instrument>();

        public Form1()
        {
            InitializeComponent();
            albumController = new AlbumController();
            instrumentController = new InstrumentController();
            performerController = new PerformerController();
            songController = new SongController();
            performerInstrumentController = new PerformerInstrumentController();
            recordLabelController = new RecordLabelController();
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != string.Empty)
                {
                    if (comboBox1.SelectedItem != null)
                    {
                        instrumentController.AddInstrument(textBox1.Text, comboBox1.SelectedItem.ToString());
                        textBox1.Clear();
                    }
                    else
                        throw new MyException("Please choose instrument type");
                }
                else
                {
                    throw new MyException("Please enter instrument");
                }
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
                if (textBox2.Text == string.Empty)
                    throw new MyException("Please enter name");

                if (textBox3.Text == string.Empty)
                    throw new MyException("Please enter country");

                recordLabelController.AddRecordLabel(textBox2.Text, textBox3.Text);

                textBox2.Clear();
                textBox3.Clear();
            }
            catch(MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Instrument instrument = instrumentController.GetInstrumentByName(textBox7.Text);
                if (instrument != null)
                {
                    listBox1.Items.Add(textBox7.Text);
                    instruments.Add(instrument);
                }
                else
                {
                    throw new MyException("This instrument doesn't exist");
                }

                textBox7.Clear();
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox4.Text == string.Empty)
                    throw new MyException("Please enter name");

                if (textBox5.Text == string.Empty)
                    throw new MyException("Please enter birth year");

                if (textBox5.Text == string.Empty)
                    throw new MyException("Please enter record label");

                RecordLabel recordLabel = recordLabelController.GetLabelByName(textBox6.Text);
                if (recordLabel == null) throw new MyException("Unable to add performer. This record label doesn't exist.");

                Performer performer = new Performer(textBox4.Text, int.Parse(textBox5.Text), recordLabel);
                performerController.AddPerformer(performer);

                if(instruments.Count() == 0)
                    throw new MyException("Please add at least one instrument");

                foreach (Instrument element in instruments)
                    performerInstrumentController.AddPerformerInstrument(performer.PerformerName, element.InstrumentName);

                instruments.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                listBox1.Items.Clear();
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string imageLocation = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png| All Files(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imageLocation = dialog.FileName;
                pictureBox1.ImageLocation = imageLocation;

                Image enteredImage = Image.FromFile(imageLocation);
                image = ImageToByteArray(enteredImage);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox8.Text == string.Empty)
                    throw new MyException("Please enter name");

                if (textBox9.Text == string.Empty)
                    throw new MyException("Please enter year");

                if (textBox10.Text == string.Empty)
                    throw new MyException("Please enter genre");

                Performer performer = performerController.GetPerformerByName(textBox11.Text);
                if (performer == null) throw new MyException("Unable to add album. This performer doesn't exist.");

                albumController.AddAlbum(textBox8.Text, int.Parse(textBox9.Text), performer, textBox10.Text, image);

                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                textBox11.Clear();
                pictureBox1.ImageLocation = "noImage.jpg";
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox12.Text == string.Empty)
                    throw new MyException("Please enter name");

                if (textBox13.Text == string.Empty)
                    throw new MyException("Please enter song duration");

                Album album = albumController.GetAlbumByName(textBox14.Text);
                if (album == null) throw new MyException("Unable to add song. This album doesn't exist.");

                songController.AddSong(textBox12.Text, double.Parse(textBox13.Text), album);

                textBox12.Clear();
                textBox13.Clear();
                textBox14.Clear();
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox15.Text == string.Empty)
                    throw new MyException("Please enter a name");

                if (textBox16.Text == string.Empty)
                    throw new MyException("Please enter an instrument");

                Performer performer = performerController.GetPerformerByName(textBox15.Text);

                if(performer == null)
                    throw new MyException("This performer doesn't exist");

                Instrument instrument = instrumentController.GetInstrumentByName(textBox16.Text);

                if (instrument == null)
                    throw new MyException("This instrument doesn't exist");

                performerInstrumentController.AddPerformerInstrument(performer.PerformerName, instrument.InstrumentName);

                textBox15.Clear();
                textBox16.Clear();
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
