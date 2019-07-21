﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Protobuf.Collections;

namespace SabberStonePython.API
{
    public partial class Game
    {
        private static int _id_gen;

        public Game(SabberStoneCore.Model.Game game, int id = -1)
        {
            player1_ = new Controller(game.Player1);
            player2_ = new Controller(game.Player2);
            state_ = (Types.State)game.State;
            turn_ = game.Turn;

            if (id < 0)
            {
                id_ = _id_gen++;
                SabberHelpers.ManagedObjects.Games.Add(id_, game);
            }
            else
            {
                id_ = id;
            }
        }

        //partial void OnConstruction()
        //{
        //    throw new NotImplementedException();
        //}
    }

    public partial class Controller
    {
        public Controller(SabberStoneCore.Model.Entities.Controller controller)
        {
            id_ = controller.PlayerId;
            hero_ = new Hero(controller.Hero);
            boardZone_ = new BoardZone(controller.BoardZone);
            handZone_ = new HandZone(controller.HandZone);
            secretZone_ = new SecretZone(controller.SecretZone);
            deckZone_ = new DeckZone(controller.DeckZone);
            playState_ = (Types.PlayState)controller.PlayState;
        }
    }

    public partial class BoardZone
    {
        public BoardZone(SabberStoneCore.Model.Zones.BoardZone boardZone)
        {
            var minions = new RepeatedField<Minion>();
            var span = boardZone.GetSpan();
            for (int i = 0; i < span.Length; i++) 
                minions.Add(new Minion(span[i]));

            minions_ = minions;
        }
    }

    public partial class HandZone
    {
        public HandZone(SabberStoneCore.Model.Zones.HandZone zone)
        {
            var playables = new RepeatedField<Playable>();
            var span = zone.GetSpan();
            for (int i = 0; i < span.Length; i++)
                playables.Add(new Playable(span[i], true));

            entities_ = playables;
        }
    }

    public partial class SecretZone
    {
        public SecretZone(SabberStoneCore.Model.Zones.SecretZone zone)
        {
            var playables = new RepeatedField<Playable>();
            var span = zone.GetSpan();
            for (int i = 0; i < span.Length; i++)
                playables.Add(new Playable(span[i], true));

            entities_ = playables;
        }
    }

    public partial class DeckZone
    {
        public DeckZone(SabberStoneCore.Model.Zones.DeckZone zone)
        {
            var playables = new RepeatedField<Playable>();
            var span = zone.GetSpan();
            for (int i = 0; i < span.Length; i++)
                playables.Add(new Playable(span[i], true));

            entities_ = playables;
        }
    }

    public partial class Minion
    {
        public Minion(SabberStoneCore.Model.Entities.Minion minion)
        {
            cardId_ = minion.Card.AssetId;
            atk_ = minion.AttackDamage;
            baseHealth_ = minion.BaseHealth;
            damage_ = minion.Damage;
            numAttacksThisTurn_ = minion.NumAttacksThisTurn;
            exhausted_ = minion.IsExhausted;
            attackableByRush_ = minion.AttackableByRush;
            charge_ = minion.HasCharge;
            windfury_ = minion.HasWindfury;
            lifesteal_ = minion.HasLifeSteal;
            poisonous_ = minion.Poisonous;
            stealth_ = minion.HasStealth;
            divineShield_ = minion.HasDivineShield;
            immune_ = minion.IsImmune;
            elusive_ = minion.CantBeTargetedBySpells;
            frozen_ = minion.IsFrozen;
            deathrattle_ = minion.HasDeathrattle;

            zonePosition_ = minion.ZonePosition;
            orderOfPlay_ = minion.OrderOfPlay;
        }
    }

    public partial class Playable
    {
        public Playable(SabberStoneCore.Model.Entities.IPlayable playable, bool hand)
        {
            cardId_ = playable.Card.AssetId;
            cost_ = playable.Cost;
            if (playable is SabberStoneCore.Model.Entities.Character c)
            {
                atk_ = c.AttackDamage;
                baseHealth_ = c.BaseHealth;
            }
            if (hand)
                ghostly_ = playable[SabberStoneCore.Enums.GameTag.GHOSTLY] == 1;
        }
    }

    public partial class Hero
    {
        public Hero(SabberStoneCore.Model.Entities.Hero hero)
        {
            cardClass_ = (int) hero.Card.Class;
            atk_ = hero.AttackDamage;
            baseHealth_ = hero.BaseHealth;
            damage_ = hero.Damage;
            numAttacksThisTurn_ = hero.NumAttacksThisTurn;
            armor_ = hero.Armor;
            exhausted_ = hero.IsExhausted;
            stealth_ = hero.HasStealth;
            immune_ = hero.IsImmune;

            power_ = new HeroPower(hero.HeroPower);
            if (hero.Weapon != null)
                weapon_ = new Weapon(hero.Weapon);
        }
    }

    public partial class HeroPower
    {
        public HeroPower(SabberStoneCore.Model.Entities.HeroPower heroPower)
        {
            cardId_ = heroPower.Card.AssetId;
            cost_ = heroPower.Cost;
            exhausted_ = heroPower.IsExhausted;
        }
    }

    public partial class Weapon
    {
        public Weapon(SabberStoneCore.Model.Entities.Weapon weapon)
        {
            cardId_ = weapon.Card.AssetId;
            atk_ = weapon.AttackDamage;
            durability_ = weapon.Durability;
            windfury_ = weapon.IsWindfury;
            lifesteal_ = weapon.HasLifeSteal;
            poisonous_ = weapon.Poisonous;
            immune_ = weapon.IsImmune;
        }
    }

    public partial class Option
    {
        public Option(SabberStoneCore.Tasks.PlayerTasks.PlayerTask playerTask, int gameId)
        {
            gameId_ = gameId;
            print_ = playerTask.ToString();

            if (playerTask is SabberStoneCore.Tasks.PlayerTasks.ChooseTask chooseTask)
            {
                choice_ = chooseTask.Choices[0];
                type_ = Types.PlayerTaskType.Choose;
                return;
            }

            type_ = (Types.PlayerTaskType)playerTask.PlayerTaskType;
            if (playerTask.HasSource)
                sourceId_ = playerTask.Source.Id;
            if (playerTask.HasTarget)
                targetId_ = playerTask.Target.Id;
            if (playerTask is SabberStoneCore.Tasks.PlayerTasks.PlayCardTask playCardTask)
            {
                subOption_ = playCardTask.ChooseOne;
                zonePosition_ = playCardTask.ZonePosition;
            }
        }
    }

    public partial class Options
    {
        public Options(List<SabberStoneCore.Tasks.PlayerTasks.PlayerTask> playerTasks, int gameId)
        {
            var options = new RepeatedField<Option>();
            options.AddRange(playerTasks.Select(p => new Option(p, gameId)));
            list_ = options;
        }
    }

    public partial class Cards
    {
        public Cards(IEnumerable<SabberStoneCore.Model.Card> allCards)
        {
            cards_ = new MapField<int, Card>();

            foreach (var card in allCards)
            {
                if (card.Name == null)
                    continue;

                cards_.Add(card.AssetId, new Card
                {
                    Id = card.AssetId,
                    Name = card.Name,
                    StringId = card.Id
                });
            }
        }
    }
}
