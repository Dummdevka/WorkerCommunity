using System;
using Domain.Entities;
using MediatR;

namespace Application.Requests.Commands.UpdateRequest
{
	public class UpdateRequestCommand : IRequest
	{
		public int? Id {
			get; set;
		}

		public Dictionary<string, object>? Data {
			get; set;
		}
		public Request? Request {
			get; set;
		}

		public UpdateRequestCommand(int id, Dictionary<string, object> data) {
			Id = id;
			Data = data;
		}

		public UpdateRequestCommand(Request request) {
			Request = request;
		}
	}
}

