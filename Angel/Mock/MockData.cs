using System;
using System.Collections.Generic;
using System.Text;

namespace Angel
{
    public static class MockData
    {
        public static Player MockPlayer { get; set; } = new Player() { Nickname = "MockData" };
        public static int MockGameTime { get; set; } = 150;
    }
}
