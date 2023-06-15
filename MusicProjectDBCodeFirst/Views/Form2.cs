using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MusicProjectDBCodeFirst.Controller;
using MusicProjectDBCodeFirst.Data;
using MusicProjectDBCodeFirst.Data.Models;
using System.IO;
using System.Linq;

namespace MusicProjectDBCodeFirst.Views
{
    public partial class Form2 : Form
    {
        AlbumController albumController;
        PerformerController performerController;
        PerformerInstrumentController performerInstrumentController;
        InstrumentController instrumentController;
        RecordLabelController recordLabelController;
        SongController songController;

        List<Album> albums;

        public Form2()
        {
            InitializeComponent();
            albumController = new AlbumController();
            performerController = new PerformerController();
            performerInstrumentController = new PerformerInstrumentController();
            instrumentController = new InstrumentController();
            recordLabelController = new RecordLabelController();
            songController = new SongController();
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
                var album = albumController.GetAlbumBySong(textBox1.Text);
                if (album == null) throw new MyException("This album doesn't exist");

                if (album.CoverImage != null)
                    pictureBox1.Image = ByteArrayToImage(album.CoverImage);

                label1.Text = textBox1.Text;

                Performer performer = performerController.GetPerformerByAlbum(album.AlbumName);
                label2.Text = performer.PerformerName;
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            pictureBox2.ImageLocation = "noImage.jpg";

            try
            {
                comboBox1.Items.Clear();

                albums = albumController.GetAlbumsByPerformer(textBox2.Text);
                if (albums == null) throw new MyException("This performer doesn't have albums yet");

                foreach (Album element in albums)
                {
                    comboBox1.Items.Add(element.ToString());
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Album element in albums)
            {
                if (comboBox1.SelectedItem.ToString() == element.ToString())
                {
                    listBox1.Items.AddRange(element.Songs.Cast<Song>().ToArray());
                    if (element.CoverImage != null)
                        pictureBox2.Image = ByteArrayToImage(element.CoverImage);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            pictureBox3.ImageLocation = "noImage.jpg";

            try
            {
                comboBox2.Items.Clear();

                albums = albumController.GetAlbumsByGenre(textBox3.Text);
                if (albums == null) throw new MyException("There are no albums with this genre");

                foreach (Album element in albums)
                {
                    comboBox2.Items.Add(element.ToString());
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            foreach (Album element in albums)
            {
                if (comboBox2.SelectedItem.ToString() == element.ToString())
                {
                    if (element.CoverImage != null)
                        pictureBox3.Image = ByteArrayToImage(element.CoverImage);
                    else
                        pictureBox2.ImageLocation = "noImage.jpg";
                    listBox2.Items.AddRange(element.Songs.Cast<Song>().ToArray());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();

            try
            {
                Album album = albumController.GetAlbumByName(textBox4.Text);
                if (album == null) throw new MyException("This album doesn't exist");

                Performer performer = performerController.GetPerformerByAlbum(album.AlbumName);

                textBox5.Text = $"By: {album.Performer}({album.Year}), Genere: {album.Genre}, Label: {performer.RecordLabel}";

                if (album.Songs.Count() != 0)
                    listBox3.Items.AddRange(album.Songs.Cast<Song>().ToArray());

                if (album.CoverImage != null)
                    pictureBox4.Image = ByteArrayToImage(album.CoverImage);
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Performer> performers = performerController.GetPerformersByRecordLabel(textBox6.Text);

            listBox4.DataSource = performers;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<PerformerInstrument> performersInstruments = performerInstrumentController.GetAll().Where(x => x.Instrument.InstrumentName == textBox7.Text).ToList();

            listBox5.DataSource = performersInstruments;
            listBox5.DisplayMember = "Performer";
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "Instruments")
            {
                var instruments = instrumentController.GetAll();

                listBox6.DataSource = instruments;
            }
            else if (comboBox3.SelectedItem.ToString() == "Record Labels")
            {
                var recordLabels = recordLabelController.GetAll();

                listBox6.DataSource = recordLabels;
            }
            else if (comboBox3.SelectedItem.ToString() == "Performers")
            {
                var performers = performerController.GetAll();

                listBox6.DataSource = performers;
            }
            else if (comboBox3.SelectedItem.ToString() == "Albums")
            {
                var albums = albumController.GetAll();

                listBox6.DataSource = albums;
            }
            else if (comboBox3.SelectedItem.ToString() == "Songs")
            {
                var songs = songController.GetAll();

                listBox6.DataSource = songs;
            }
        }
    }
}
