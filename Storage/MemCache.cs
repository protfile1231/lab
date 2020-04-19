using System;
using System.Collections.Generic;
using System.Linq;
using lab.Models;

namespace lab.Storage
{
    public class MemCache : IStorage<labData>
    {
        private object _sync = new object();
        private List<labData> _memCache = new List<labData>();
        public labData this[Guid id] 
        { 
            get
            {
                lock (_sync)
                {
                    if (!Has(id)) throw new IncorrectlabDataException($"No LabData with id {id}");

                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty) throw new IncorrectlabDataException("Cannot request LabData with an empty id");

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _memCache.Add(value);
                }
            }
        }

        public System.Collections.Generic.List<labData> All => _memCache.Select(x => x).ToList();

        public void Add(labData value)
        {
            if (value.Id != Guid.Empty) throw new IncorrectlabDataException($"Cannot add value with predefined id {value.Id}");

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _memCache.Any(x => x.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _memCache.RemoveAll(x => x.Id == id);
            }
        }
    }
}