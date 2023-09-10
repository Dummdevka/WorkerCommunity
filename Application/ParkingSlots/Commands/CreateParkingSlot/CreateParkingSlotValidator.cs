using System;
using FluentValidation;

namespace Application.ParkingSlots.Commands.CreateParkingSlot
{
	public class CreateParkingSlotValidator : AbstractValidator<CreateParkingSlotCommand>
	{
		public CreateParkingSlotValidator()
		{
			RuleFor(s => s.name)
				.NotEmpty()
				.MaximumLength(10);
		}
	}
}

