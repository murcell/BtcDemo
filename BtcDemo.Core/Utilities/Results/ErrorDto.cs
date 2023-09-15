﻿namespace BtcDemo.Core.Utilities.Results;

public class ErrorDto
{
	public ErrorDto()
	{
		Errors = new List<string>();
	}

	public List<String> Errors { get; set; }
	public int Status { get; set; }
}
