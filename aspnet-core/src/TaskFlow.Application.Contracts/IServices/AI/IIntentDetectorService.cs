using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Enums;

namespace TaskFlow.IServices.AI
{
    public interface IIntentDetectorService
    {
        IntentType Detect(string question);
    }
}
