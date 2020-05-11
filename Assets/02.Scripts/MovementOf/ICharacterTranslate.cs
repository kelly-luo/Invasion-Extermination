﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterTranslate
{
    Transform Character { get; set; }

    float Speed { get; set; }

    bool IsRunning { get; set; }

    bool IsSitting { get; set; }

    void TranslateCharacter(Vector3 moveDir);

}
