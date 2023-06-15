using System;
using System.Collections.Generic;
using System.Text;
using MusicProjectDBCodeFirst.Data;
using System.Linq;
using MusicProjectDBCodeFirst.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicProjectDBCodeFirst.Controller
{
    public class PerformerInstrumentController
    {
        private MusicProjectDBContext context;

        public PerformerInstrumentController()
        {
            context = new MusicProjectDBContext();
        }

        public void AddPerformerInstrument(string performerName, string instrumentName)
        {
            var performer = context.Performers
               .FirstOrDefault(x => x.PerformerName == performerName);

            var instrument = context.Instruments
                .FirstOrDefault(x => x.InstrumentName == instrumentName);

            context.PerformersInstruments.Add(new PerformerInstrument { PerformerId = performer.Id, InstrumentId = instrument.Id });
            context.SaveChanges();
        }

        public List<PerformerInstrument> GetAll()
        {
            return context.PerformersInstruments.Include(x => x.Instrument).Include(y => y.Performer).ToList();
        }

        public void DeleteByInstrumentName(string name)
        {
            var records = context.PerformersInstruments.Where(y => y.Instrument.InstrumentName == name);

            if (records != null)
                foreach (var element in records)
                    context.PerformersInstruments.Remove(element);
            context.SaveChanges();
        }

        public void DeleteByPerformerName(string name)
        {
            var records = context.PerformersInstruments.Where(y => y.Performer.PerformerName == name);

            if (records != null)
                foreach (var element in records)
                    context.PerformersInstruments.Remove(element);
            context.SaveChanges();
        }
    }
}
