using System;
using System.Collections.Generic;
using System.Text;
using MusicProjectDBCodeFirst.Data;
using MusicProjectDBCodeFirst.Data.Models;
using System.Windows.Forms;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MusicProjectDBCodeFirst.Controller
{
    public class InstrumentController
    {
        private MusicProjectDBContext context;

        public InstrumentController()
        {
            context = new MusicProjectDBContext();
        }

        public void AddInstrument(string name, string type)
        {
            context.Instruments.Add(new Instrument { InstrumentName = name, InstrumentType = type });
            context.SaveChanges();
        }

        public Instrument GetInstrumentByName(string name)
        {
            return context.Instruments.FirstOrDefault(i => i.InstrumentName == name);
        }

        public List<Instrument> GetInstrumentByPerformer(string performerName)
        {
            var instrumentsPerformers =  context.PerformersInstruments.Include(p => p.Performer).Include(i => i.Instrument).Where(p => p.Performer.PerformerName == performerName).ToList();
            return instrumentsPerformers.Select(x => x.Instrument).ToList();
        }

        public void DeleteInstrument(string name)
        {
            context.Instruments.Remove(context.Instruments.FirstOrDefault(y => y.InstrumentName == name));
            context.SaveChanges();
        }

        public virtual ICollection<Instrument> GetAll()
        {
            return context.Instruments.ToList();
        }
    }
}
