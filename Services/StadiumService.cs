﻿using System.Collections.Generic;
using Com.Dotnet.Cric.Repositories;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Stadiums;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Services
{
	public class StadiumService
	{
		private readonly StadiumRepository stadiumRepository;

		public StadiumService(StadiumRepository stadiumRepository)
		{
			this.stadiumRepository = stadiumRepository;
		}

		public Stadium Create(CreateRequest createRequest)
		{
			createRequest.Validate();

			var existingStadium = stadiumRepository.GetByNameAndCountryId(createRequest.Name, createRequest.CountryId);
			if (null != existingStadium)
			{
				throw new ConflictException("Stadium");
			}

			return stadiumRepository.Create(createRequest);
		}

		public List<Stadium> GetAll(int page, int limit)
		{
			return stadiumRepository.GetAll(page, limit);
		}

		public int GetTotalCount()
		{
			return stadiumRepository.GetTotalCount();
		}

		public Stadium GetById(long id)
		{
			return stadiumRepository.GetById(id);
		}
	}
}