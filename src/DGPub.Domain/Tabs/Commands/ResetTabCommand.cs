﻿using DGPub.Domain.Core;
using System;

namespace DGPub.Domain.Tabs.Commands
{
    public class ResetTabCommand : Command
    {
        public ResetTabCommand(Guid tabId)
        {
            TabId = tabId;
        }

        public Guid TabId { get; set; }
        public override bool IsValid()
        {
            return TabId != Guid.Empty;
        }
    }
}
