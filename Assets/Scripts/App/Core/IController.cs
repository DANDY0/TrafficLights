﻿namespace App.Core
{
    public interface IController
    {
        void Init();

        void Update();

        void Dispose();

        void ResetAll();
    }
}
