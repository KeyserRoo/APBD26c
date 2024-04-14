namespace API
{
	public class MockDB : IMockDB
	{
		private ICollection<Zwierzontko> _zwierzontka;
		private ICollection<Wizyta> _wizyty;
		public MockDB()
		{
			_zwierzontka = new List<Zwierzontko>
			{
				new Zwierzontko()
				{
					Id = 1,
					Name = "Kacper",
					Mass = 13,
					Color = "rzułty",
					Category = "Pies"
				},
				new Zwierzontko()
				{
					Id = 2,
					Name = "Krzysztof",
					Mass = 20,
					Color = "czarny",
					Category = "Kot"
				}
			};
			_wizyty = new List<Wizyta>{
				new Wizyta(){
					Zwierze = _zwierzontka.ElementAt(1),
					Data = "dzis",
					Opis = "3 minuty :)",
					Cena = 200
				}
			};
		}
		public ICollection<Zwierzontko> GetAll()
		{
			return _zwierzontka;
		}
		public ICollection<Wizyta>? GetWizytyByZwierzId(int id)
		{
			ICollection<Wizyta> toReturn = new List<Wizyta>();
			foreach (var wizyta in _wizyty)
			{
				if (wizyta.Zwierze.Id == id) toReturn.Add(wizyta);
			}
			if (toReturn.Count == 0) return null;
			else return toReturn;
		}
		public Zwierzontko? GetById(int id)
		{
			return _zwierzontka.FirstOrDefault(zwierzontko => zwierzontko.Id == id);
		}
		public bool Add(Zwierzontko zwierzontko)
		{
			bool canAdd = true;
			foreach (var zwierz in _zwierzontka)
			{
				if (zwierzontko.Id == zwierz.Id)
				{
					canAdd = false;
					break;
				}
			}
			if (canAdd)
			{
				_zwierzontka.Add(zwierzontko);
			}
			return canAdd;
		}
		public bool Add(Wizyta wizyta)
		{
			bool canAdd = true;
			foreach (var item in _wizyty)
			{
				if (wizyta.Equals(item))
				{
					canAdd = false;
					break;
				}
			}
			if (canAdd) _wizyty.Add(wizyta);
			return canAdd;
		}
		public void EditOrAdd(int id, Zwierzontko zwierzontko)
		{
			foreach (var zwierz in _zwierzontka)
			{
				if (zwierz.Id == id)
				{
					_zwierzontka.Remove(zwierz);
					break;
				}
			}

			//ta linijka wynika z kiepskiego ustrukturyzowania rzeczy wymuszonego zadaniem,
			//jedno id podajemy w luzem, drugie jako czesc zwierzecia,
			//tutaj upewniam sie ze jest to to samo, a wazniejsze jest to podane w endpoincie
			zwierzontko.Id = id;
			_zwierzontka.Add(zwierzontko);
		}
		public bool Delete(int id)
		{
			foreach (var zwierz in _zwierzontka)
			{
				if (zwierz.Id == id)
				{
					_zwierzontka.Remove(zwierz);
					return true;
				}
			}
			return false;
		}
	}

	public interface IMockDB
	{
		public ICollection<Zwierzontko> GetAll();
		public ICollection<Wizyta> GetWizytyByZwierzId(int id);
		public Zwierzontko? GetById(int id);
		public bool Add(Zwierzontko zwierzontko);
		public bool Add(Wizyta wizyta);
		public void EditOrAdd(int id, Zwierzontko zwierzontko);
		public bool Delete(int id);
	}
	public class Zwierzontko
	{
		public int Id { get; set; } = 0;
		public int Mass { get; set; } = 0;
		public string Name { get; set; } = "";
		public string Category { get; set; } = "";
		public string Color { get; set; } = "";
	}
	public class Wizyta
	{
		public Zwierzontko Zwierze { get; set; } = null;
		public string Data { get; set; } = "";
		public string Opis { get; set; } = "";
		public int Cena { get; set; } = 0;
	}
}