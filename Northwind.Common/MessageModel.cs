using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Chat.Models;

public class MessageModel
{
    public string From { get; set; } = null!;
    public string To { get; set; } = null!;
    public string? Body { get; set; }
}
