﻿using BCCL.MvvmLight;
using TEditXNA.Terraria;
using System.Windows.Input;
using TEditXNA.Terraria.Objects;

namespace TEditXna.Editor
{
    public class TilePicker : ObservableObject
    {
        private PaintMode _paintMode = ToolDefaultData.PaintMode;
        private MaskMode _tileMaskMode = ToolDefaultData.PaintTileMaskMode;
        private MaskMode _wallMaskMode = ToolDefaultData.PaintWallMaskMode;
        private int _wall = ToolDefaultData.PaintWall;
        private int _tile = ToolDefaultData.PaintTile;
        private int _wallMask = ToolDefaultData.PaintWallMask;
        private int _tileMask = ToolDefaultData.PaintTileMask;
        //private bool _isLava;
        private bool _isEraser;
        private Liquid _liquid = Liquid.Water;

        public bool IsEraser
        {
            get { return _isEraser; }
            set { Set("IsEraser", ref _isEraser, value); }
        }

        public bool IsLava
        {
            get { return _liquid.HasFlag(Liquid.Lava); }
            set
            {
                _liquid = value ? Liquid.Lava : Liquid.Water;
                RaisePropertyChanged("IsWater");
                RaisePropertyChanged("IsLava");
                RaisePropertyChanged("IsHoney");
            }
        }

        public bool IsWater
        {
            get { return _liquid.HasFlag(Liquid.Water); }
            set
            {
                _liquid = !value ? Liquid.Lava : Liquid.Water;
                RaisePropertyChanged("IsWater");
                RaisePropertyChanged("IsLava");
                RaisePropertyChanged("IsHoney");
            }
        }

        public bool IsHoney
        {
            get { return _liquid.HasFlag(Liquid.Honey); }
            set
            {
                _liquid = value ? Liquid.Honey : Liquid.Water;
                RaisePropertyChanged("IsWater");
                RaisePropertyChanged("IsLava");
                RaisePropertyChanged("IsHoney");
            }
        }

        public int TileMask
        {
            get { return _tileMask; }
            set { Set("TileMask", ref _tileMask, value); }
        }

        public int WallMask
        {
            get { return _wallMask; }
            set { Set("WallMask", ref _wallMask, value); }
        }

        public int Tile
        {
            get { return _tile; }
            set { Set("Tile", ref _tile, value); }
        }

        public int Wall
        {
            get { return _wall; }
            set { Set("Wall", ref _wall, value); }
        }

        public MaskMode WallMaskMode
        {
            get { return _wallMaskMode; }
            set { Set("WallMaskMode", ref _wallMaskMode, value); }
        }

        public MaskMode TileMaskMode
        {
            get { return _tileMaskMode; }
            set { Set("TileMaskMode", ref _tileMaskMode, value); }
        }

        public PaintMode PaintMode
        {
            get { return _paintMode; }
            set { Set("PaintMode", ref _paintMode, value); }
        }

        public void Swap(ModifierKeys modifier)
        {
            switch (PaintMode)
            {
                case PaintMode.Tile:
                    SwapTile();
                    break;
                case PaintMode.Wall:
                    SwapWall();
                    break;
                case PaintMode.TileAndWall:
                    if (modifier.HasFlag(ModifierKeys.Shift))
                        SwapWall();
                    else
                        SwapTile();
                    break;
                case PaintMode.Liquid:
                    SwapLiquid();
                    break;
            }
        }

        public void SwapTile()
        {
            int currentTile = Tile;
            Tile = TileMask;
            TileMask = currentTile;
        }

        public void SwapWall()
        {
            int currentWall = Wall;
            Wall = WallMask;
            WallMask = currentWall;
        }

        public void SwapLiquid()
        {
            switch (_liquid)
            {
                case Liquid.Lava: _liquid = Liquid.Honey; break;
                case Liquid.Water: _liquid = Liquid.Lava; break;
                default: _liquid = Liquid.Water; break;
            }

            RaisePropertyChanged("IsWater");
            RaisePropertyChanged("IsLava");
            RaisePropertyChanged("IsHoney");
        }
    }
}