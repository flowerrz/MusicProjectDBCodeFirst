using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MusicProjectDBCodeFirst.Data;
using MusicProjectDBCodeFirst.Data.Models;
using System.Windows.Forms;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace MusicProjectDBCodeFirst.Controller
{
    public class PerformerController
    {
        private MusicProjectDBContext context;

        public PerformerController()
        {
            context = new MusicProjectDBContext();
        }

        public void AddPerformer(Performer performer)
        {
            context.Performers.Add(new Performer{
                PerformerName = performer.PerformerName,
                BirthYear = performer.BirthYear,
                //h
                RecordLabelId = performer.RecordLabel.Id
            });
            context.SaveChanges();
        }

        public Performer GetPerformerByName(string name)
        {
            return context.Performers.FirstOrDefault(p => p.PerformerName == name);
        }

        public Performer GetPerformerByAlbum(string album)
        {
            return context.Performers.Include(l => l.RecordLabel).FirstOrDefault(p => p.Albums.Any(a => a.AlbumName == album));
        }

        public List<Performer> GetPerformersByRecordLabel(string label)
        {
            return context.Performers.Include(r => r.RecordLabel).Where(x => x.RecordLabel.LabelName == label).ToList();
        }

        public virtual ICollection<Performer> GetAll()
        {
            return context.Performers.ToList();
        }

        public void UpdateLabel(Performer performer, RecordLabel label)
        {
            performer.RecordLabel = label;
            context.SaveChanges();
        }

        public void DeletePerformer(string name)
        {
            context.Performers.Remove(context.Performers.FirstOrDefault(p => p.PerformerName == name));
            context.SaveChanges();
        }

        public void ChangeName(Performer performer, string newName)
        {
            performer.PerformerName = newName;
            context.SaveChanges();
        }
    }
}
