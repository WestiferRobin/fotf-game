using System;
using System.Collections;
using System.Collections.Generic;
using FotF.Api.Games.BattleGames.UnitBattles;
using FotF.Api.Terminal;
using FotF.Terminal.Games;
using UnityEngine;

public class TerminalManager : MonoBehaviour
{
    public PlayerTerminal terminal;
    public IGame game = new TrooperBattle(1);

    public void Awake()
    {
        terminal = new UnityTerminal(game);
    }

    public void Start()
    {
        terminal.Run();
    }
}
