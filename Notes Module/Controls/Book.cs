﻿using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nekres.Notes_Module.Controls
{
    public class Book : Container
    {
        private readonly BitmapFont TitleFont = GameService.Content.GetFont(ContentService.FontFace.Menomonia, ContentService.FontSize.Size32, ContentService.FontStyle.Regular);
        private readonly Texture2D TurnPageSprite;

        private static int RIGHT_PADDING = 150;
        private static int TOP_PADDING = 100;
        private static int SHEET_OFFSET_Y = 20;

        private Rectangle _leftButtonBounds;
        private Rectangle _rightButtonBounds;
        private Rectangle _titleBounds;

        private bool _mouseOverTurnPageLeft;
        private bool _mouseOverTurnPageRight;

        private List<Page> Pages = new List<Page>();

        private string _title = "No Title";
        /// <summary>
        /// Sets the title of this book.
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                if (value.Equals(_title)) return;
                SetProperty(ref _title, value, true);
            }
        }
        /// <summary>
        /// The currently open page of this book.
        /// </summary>
        public Page CurrentPage { get; private set; }
        /// <summary>
        /// Creates a panel that should act as Parent for Page controls to create a book UI.
        /// </summary>
        public Book()
        {
            TurnPageSprite = TurnPageSprite ?? GameService.Content.GetTexture("1909317");
        }
        protected override void OnResized(ResizedEventArgs e)
        {
            ContentRegion = new Rectangle(0, 0, e.CurrentSize.X, e.CurrentSize.Y);

            _leftButtonBounds = new Rectangle(25, (ContentRegion.Height - TurnPageSprite.Bounds.Height) / 2 + SHEET_OFFSET_Y, TurnPageSprite.Bounds.Width, TurnPageSprite.Bounds.Height);
            _rightButtonBounds = new Rectangle(ContentRegion.Width - TurnPageSprite.Bounds.Width - 25, (ContentRegion.Height - TurnPageSprite.Bounds.Height) / 2 + SHEET_OFFSET_Y, TurnPageSprite.Bounds.Width, TurnPageSprite.Bounds.Height);

            var titleSize = (Point)TitleFont.MeasureString(_title);
            _titleBounds = new Rectangle((ContentRegion.Width - titleSize.X) / 2, ContentRegion.Top + (TOP_PADDING - titleSize.Y) / 2, titleSize.X, titleSize.Y);

            if (Pages != null && Pages.Count > 0) {
                foreach (Page page in this.Pages)
                {
                    if (page == null) continue;
                    page.Size = PointExtensions.ResizeKeepAspect(page.Size, ContentRegion.Width - RIGHT_PADDING, ContentRegion.Height - TOP_PADDING, true);
                    page.Location = new Point((ContentRegion.Width - page.Size.X) / 2, (ContentRegion.Height - page.Size.Y) / 2 + SHEET_OFFSET_Y);
                }
            }

            base.OnResized(e);
        }
        protected override void OnHidden(EventArgs e)
        {
            //TODO: Add gw2 book sound: close book.
            base.OnHidden(e);
        }
        protected override void OnShown(EventArgs e)
        {
            //TODO: Add gw2 book sound: open book.
            base.OnShown(e);
        }
        protected override void OnChildAdded(ChildChangedEventArgs e)
        {
            if (e.ChangedChild is Page page && !Pages.Any(x => x.Equals((Page)e.ChangedChild)))
            {
                page.Size = PointExtensions.ResizeKeepAspect(page.Size, ContentRegion.Width - RIGHT_PADDING, ContentRegion.Height - TOP_PADDING, true);
                page.Location = new Point((ContentRegion.Width - page.Size.X) / 2, (ContentRegion.Height - page.Size.Y) / 2 + SHEET_OFFSET_Y);
                page.PageNumber = Pages.Count + 1;
                Pages.Add(page);

                if (Pages.Count == 1) CurrentPage = page;
                if (page != CurrentPage) page.Hide();
            }

            base.OnChildAdded(e);
        }
        protected override void OnMouseMoved(MouseEventArgs e)
        {
            var relPos = RelativeMousePosition;

            _mouseOverTurnPageLeft = _leftButtonBounds.Contains(relPos);
            _mouseOverTurnPageRight = _rightButtonBounds.Contains(relPos);

            base.OnMouseMoved(e);
        }
        protected override void OnLeftMouseButtonPressed(MouseEventArgs e)
        {
            if (_mouseOverTurnPageLeft)
            {
                TurnPage(Pages.IndexOf(CurrentPage) - 1);
            }
            else if (_mouseOverTurnPageRight)
            {
                TurnPage(Pages.IndexOf(CurrentPage) + 1);
            }

            base.OnLeftMouseButtonPressed(e);
        }
        private void TurnPage(int index)
        {
            if (index >= 0 && index < Pages.Count)
            {
                // TODO: Add gw2's book sounds: turn page
                CurrentPage = Pages[index];

                foreach (Page other in Pages)
                {
                    other.Visible = other == CurrentPage;
                }
            }
        }
        public override void PaintBeforeChildren(SpriteBatch spriteBatch, Rectangle bounds)
        {
            base.PaintBeforeChildren(spriteBatch, bounds);

            // TODO: Title background texture from the original.

            // Draw title
            spriteBatch.DrawStringOnCtrl(this, _title, TitleFont, _titleBounds, Color.White, false, HorizontalAlignment.Left, VerticalAlignment.Top);

            // Draw turn page buttons
            if (!_mouseOverTurnPageLeft)
            {
                spriteBatch.DrawOnCtrl(this, TurnPageSprite, _leftButtonBounds, TurnPageSprite.Bounds, new Color(155, 155, 155, 155), 0, Vector2.Zero, SpriteEffects.FlipHorizontally);
            }
            else
            {
                spriteBatch.DrawOnCtrl(this, TurnPageSprite, _leftButtonBounds, TurnPageSprite.Bounds, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally);
            }

            if (!_mouseOverTurnPageRight)
            {
                spriteBatch.DrawOnCtrl(this, TurnPageSprite, _rightButtonBounds, TurnPageSprite.Bounds, new Color(155, 155, 155, 155));
            }
            else
            {
                spriteBatch.DrawOnCtrl(this, TurnPageSprite, _rightButtonBounds, TurnPageSprite.Bounds, Color.White);
            }
        }
    }
}
