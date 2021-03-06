﻿using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FWCB2014.Domain.Infrastructure.Repositories
{
  public class JsonCountryRepository : IRepository<CountryModel>
  {
    private readonly string _jsonPath;

    private IEnumerable<CountryModel> _countries;
    private IEnumerable<CountryModel> Countries
    {
      get
      {
        if (_countries == null)
          _countries = GetCountries();
        return _countries;
      }
    }

    private IEnumerable<CountryModel> GetCountries()
    {
      var json = File.ReadAllText(_jsonPath);
      var countries = JsonConvert.DeserializeObject<List<CountryModel>>(json);
      return countries;
    }

    public JsonCountryRepository(string jsonPath)
    {
      _jsonPath = jsonPath;
    }

    public CountryModel Find(string code)
    {
      var country = Countries.First(e => e.Code == code);
      // hack
      if (country.Name == "England")
        country.Alpha2Code = "England";
      // /hack
      return country;
    }
  }
}
