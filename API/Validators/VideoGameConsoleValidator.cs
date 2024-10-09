using FluentValidation;
using MapperAdapter.Dto.Request;

namespace API.Validators
{
    public class VideoGameConsoleValidator : AbstractValidator<VideoGameConsoleRequestDto>
    {
        public VideoGameConsoleValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage("El nombre no debe estar vacío");

            RuleFor(dto => dto.LaunchDate)
                .NotEmpty().WithMessage("Debe colocar la fecha de lanzamiento");
        }
    }
}
