using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorConfigurations  {

    public string colorName;

    public Sprite[] shipsColor;
    public Sprite[] lifesColor;
    public Sprite powerUpBoltColor;
    public Sprite powerUpShieldColor;
    public Sprite powerUpStarColor;
    public Sprite pillColor;
    public Sprite menuColor;
    public Sprite buttonColor;

    public ColorConfigurations(string _colorName,  Sprite[] _shipsColor, Sprite[] _lifesColor, Sprite _powerUpBoltColor, Sprite _powerUpShieldColor, Sprite _powerUpStarColor, Sprite _pillColor, Sprite _menuColor, Sprite _buttonColor)
    {
        colorName = _colorName;
        shipsColor = _shipsColor;
        lifesColor = _lifesColor;
        powerUpBoltColor = _powerUpBoltColor;
        powerUpShieldColor = _powerUpShieldColor;
        powerUpStarColor = _powerUpStarColor;
        pillColor = _pillColor;
        menuColor = _menuColor;
        buttonColor = _buttonColor;
    }
}
