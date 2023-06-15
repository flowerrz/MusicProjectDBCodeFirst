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
using Microsoft.EntityFrameworkCore;
using MusicProjectDBCodeFirst.Controller;
using MusicProjectDBCodeFirst.Data;
using MusicProjectDBCodeFirst.Data.Models;

namespace MusicProjectDBCodeFirst.Views
{
    public partial class Form4 : Form
    {
        AlbumController albumController;
        InstrumentController instrumentController;
        PerformerController performerController;
        SongController songController;
        PerformerInstrumentController performerInstrumentController;
        RecordLabelController recordLabelController;

        public Form4()
        {
            InitializeComponent();
            albumController = new AlbumController();
            instrumentController = new InstrumentController();
            performerController = new PerformerController();
            songController = new SongController();
            performerInstrumentController = new PerformerInstrumentController();
            recordLabelController = new RecordLabelController();
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult message = MessageBox.Show("Are you sure you want to delete this record label", "Warning", MessageBoxButtons.OKCancel);
                if (message == DialogResult.OK)
                {
                    var recordLabel = recordLabelController.GetLabelByName(textBox2.Text);

                    if (recordLabel == null)
                        throw new MyException("This label doesn't exist");

                    List<Performer> performers = performerController.GetPerformersByRecordLabel(recordLabel.LabelName);

                    if (performers.Count() > 0)
                        throw new MyException("Plese change the performer's record label before deleting it");

                    recordLabelController.DeleteRecordLabel(textBox2.Text);

                    textBox2.Clear();
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            var albums = albumController.GetAll();
            var albumNames = albums.Select(a => a.AlbumName);

            if (albumNames.Contains(textBox8.Text))
            {
                Album album = albums.FirstOrDefault(x => x.AlbumName == textBox8.Text);
                if (album.CoverImage != null)
                    pictureBox1.Image = ByteArrayToImage(album.CoverImage);
            }
            else
                pictureBox1.Image = Image.FromFile("noImage.jpg");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult message = MessageBox.Show("Are you sure you want to delete this album", "Warning", MessageBoxButtons.OKCancel);
                if (message == DialogResult.OK)
                {
                    if (textBox8.Text == string.Empty)
                        throw new Exception("Please enter album");

                    var album = albumController.GetAlbumByName(textBox8.Text);
                    if (album == null)
                        throw new MyException("This album, doesn't exist");

                    var albumToDelete = albumController.GetAlbumByName(textBox8.Text);

                    albumController.DeleteAlbum(albumToDelete.AlbumName);

                    textBox8.Clear();
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult message = MessageBox.Show("Are you sure you want to delete this instrument", "Warning", MessageBoxButtons.OKCancel);
                if (message == DialogResult.OK)
                {
                    var instrument = instrumentController.GetInstrumentByName(textBox1.Text);

                    if (instrument == null)
                        throw new MyException("This instrument doesn't exist");

                    performerInstrumentController.DeleteByInstrumentName(textBox1.Text);
                    instrumentController.DeleteInstrument(textBox1.Text);

                    textBox1.Clear();
                }
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
                DialogResult message = MessageBox.Show("Are you sure you want to delete this performer", "Warning", MessageBoxButtons.OKCancel);
                if (message == DialogResult.OK)
                {
                    var performer = performerController.GetPerformerByName(textBox4.Text);

                    if (performer == null)
                        throw new MyException("This performer doesn't exist");

                    performerInstrumentController.DeleteByPerformerName(textBox4.Text);
                    performerController.DeletePerformer(textBox4.Text);

                    textBox4.Clear();
                }
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
                DialogResult message = MessageBox.Show("Are you sure you want to delete this song", "Warning", MessageBoxButtons.OKCancel);
                if (message == DialogResult.OK)
                {
                    var song = songController.GetSongByName(textBox12.Text);

                    if (song == null)
                        throw new MyException("This song doesn't exist");

                    songController.DeleteSong(textBox12.Text);

                    textBox12.Clear();
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
