using EnterpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class AddVideoGameConsole<TDTO>
    {
        private readonly IRepository<VideoGameConsole> _repository;
        private readonly IMapper<TDTO, VideoGameConsole> _mapper;

        public AddVideoGameConsole(IRepository<VideoGameConsole> repository,
            IMapper<TDTO, VideoGameConsole> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task ExecuteAsync(TDTO consoleDto)
        {
            var console = _mapper.Map(consoleDto);
            if (string.IsNullOrEmpty(console.Name))
                throw new Exception("El Nombre no puede ser vacio");

            await _repository.AddAsync(console);

        }

    }
}
