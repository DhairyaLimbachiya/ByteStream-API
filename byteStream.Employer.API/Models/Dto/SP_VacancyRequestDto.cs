﻿namespace byteStream.Employer.API.Models.Dto
{
    public class SP_VacancyRequestDto
    {
        public Guid VacancyId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
