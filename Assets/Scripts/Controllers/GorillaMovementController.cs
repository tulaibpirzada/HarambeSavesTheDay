using UnityEngine;
using System.Collections;

public class GorillaMovementController : Singleton<GorillaMovementController>  {

	GameReferences gameRef;
	private bool shouldAllowGorillaMovement;

	public void Init(GameObject gameObject)
	{
		gameRef = gameObject.GetComponent<GameReferences> ();
		shouldAllowGorillaMovement = true;
	}

	void FixedUpdate()
	{
		if (shouldAllowGorillaMovement)
		{
			int tapCount = Input.touchCount;
			if (tapCount > 0) {
				Touch touch = Input.GetTouch (0);
				if (touch.phase == TouchPhase.Began || (Input.GetMouseButtonDown (0))) {
					
					Vector3 worldPoint = Camera.main.ScreenToWorldPoint (touch.position);
					GameObject leftMovementControl = gameRef.leftMovementControl;
					GameObject rightMovementControl = gameRef.rightMovementControl;
					if (leftMovementControl.GetComponent<Collider2D> ().OverlapPoint (worldPoint)) {
						
					} else if (leftMovementControl.GetComponent<Collider2D> ().OverlapPoint (worldPoint)) {
						
					}

				}
			}

//			for (int i = 0; i < tapCount; i++) 
//			{
//				Touch touch = Input.GetTouch (i);
//				if (touch.phase == TouchPhase.Began || (Input.GetMouseButtonDown(0)))
//				{
//					Debug.Log ("Touch Began # " + i);
//					Vector3 worldPoint = Camera.main.ScreenToWorldPoint (touch.position);
//					for (int j = 0; j < cardMovementRef.playerCards.Length; j++) 
//					{
//						GameObject card = cardMovementRef.playerCards [j];
//						if (card.GetComponent<Collider2D> ().OverlapPoint (worldPoint)) 
//						{
//							Debug.Log ("Inside");
//
//							if (player1Card == null && j < cardMovementRef.playerCards.Length / 2)
//							{
//								player1Card = card;
//								GameModel.Instance.IsPlayer1CardHeld = true;
//								player1CardInitialPosition = card.transform.position;
//								tappedCardsDictionary.Add (touch.fingerId, card);
//								Debug.Log ("Player Card 1 Assigned");
//								Debug.Log ("Touch # " + touch.fingerId);
//								Debug.Log ("Card # " + j);
//								break;
//							} 
//							else if (player2Card == null && j < cardMovementRef.playerCards.Length)
//							{
//								player2Card = card;
//								GameModel.Instance.IsPlayer2CardHeld = true;
//								player2CardInitialPosition = card.transform.position;
//								tappedCardsDictionary.Add (touch.fingerId, card);
//								Debug.Log ("Player Card 2 Assigned");
//								Debug.Log ("Touch # " + touch.fingerId);
//								Debug.Log ("Card # " + j);
//								break;
//							}
//							else if(player1Card != null)
//							{
//								Debug.Log ("Player Card 1 Not Null");
//								Debug.Log ("Card # " + j);
//							}
//							else if(player2Card != null)
//							{
//								Debug.Log ("Player Card 2 Not Null");
//								Debug.Log ("Card # " + j);
//							}
//
//						} 
//
//					}
//
//				}
//
//				if (tappedCardsDictionary.ContainsKey (touch.fingerId) && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)) 
//				{
//					Vector3 worldPoint = Camera.main.ScreenToWorldPoint (touch.position);
//					GameObject card = null;
//					if (tappedCardsDictionary.TryGetValue (touch.fingerId, out card))
//					{
//						Rigidbody2D rb = card.GetComponent<Rigidbody2D> ();
//						rb.position = worldPoint;
//						if (card.Equals (player1Card))
//						{
//							rb.position = new Vector2 (
//								Mathf.Clamp (rb.position.x, cardMovementRef.player1Boundary.xMin, cardMovementRef.player1Boundary.xMax), 
//								Mathf.Clamp (rb.position.y, cardMovementRef.player1Boundary.yMin, cardMovementRef.player1Boundary.yMax));
//						}
//						else if (card.Equals (player2Card))
//						{
//							rb.position = new Vector2 (
//								Mathf.Clamp (rb.position.x, cardMovementRef.player2Boundary.xMin, cardMovementRef.player2Boundary.xMax), 
//								Mathf.Clamp (rb.position.y, cardMovementRef.player2Boundary.yMin, cardMovementRef.player2Boundary.yMax));
//						}
//					}
//
//
//				}
//
//				if (tappedCardsDictionary.ContainsKey (touch.fingerId) && touch.phase == TouchPhase.Ended) 
//				{
//					Debug.Log ("Touch End # " + touch.fingerId);
//					GameObject card = null;
//					if (tappedCardsDictionary.TryGetValue (touch.fingerId, out card)) 
//					{
//						if (card.Equals (player1Card)) 
//						{
//							player1Card = null;
//							Debug.Log ("Player Card 1 Nullified");
//							GameModel.Instance.IsPlayer1CardHeld = false;
//							//							CharacterMover characterMover = new CharacterMover (Constants.PLAYER_1, card, player1CardInitialPosition);
//							if(GameModel.Instance.HasPlayer1SelectedPath1 || GameModel.Instance.HasPlayer1SelectedPath2 || GameModel.Instance.HasPlayer1SelectedPath3)
//							{
//								CardReferences cardRef = card.GetComponent<CardReferences> ();
//								if (cardRef.character != null && cardRef.spawningCost <= GameModel.Instance.Player1Revenue)
//								{
//									GameObject character = Instantiate(cardRef.character, card.transform.position, card.transform.rotation) as GameObject;
//									//									character.GetComponent<CharacterMover> ().playerType = Constants.PLAYER_1;
//									GameController.Instance.WithdrawFromPlayerRevenue (Constants.PLAYER_1, cardRef.spawningCost);
//									//									character.GetComponent<CharacterMover> ().characterCard = card;
//									Debug.Log ("Character Initialized");
//								}
//							}
//							card.transform.position = player1CardInitialPosition;
//							Debug.Log ("Card Repositioned");
//						} 
//						else if (card.Equals (player2Card)) 
//						{
//							player2Card = null;
//							Debug.Log ("Player Card 2 Nullified");
//							GameModel.Instance.IsPlayer2CardHeld = false;
//							//							CharacterMover characterMover = new CharacterMover (Constants.PLAYER_2, card, player2CardInitialPosition);
//							if(GameModel.Instance.HasPlayer2SelectedPath1 || GameModel.Instance.HasPlayer2SelectedPath2 || GameModel.Instance.HasPlayer2SelectedPath3)
//							{
//								CardReferences cardRef = card.GetComponent<CardReferences> ();
//								if (cardRef.character != null && cardRef.spawningCost <= GameModel.Instance.Player2Revenue)
//								{
//									GameObject character = Instantiate(cardRef.character, card.transform.position, card.transform.rotation) as GameObject;
//									//									character.GetComponent<CharacterMover> ().playerType = Constants.PLAYER_2;
//									GameController.Instance.WithdrawFromPlayerRevenue (Constants.PLAYER_2, cardRef.spawningCost);
//									//									character.GetComponent<CharacterMover> ().characterCard = card;
//									Debug.Log ("Character Initialized");
//								}
//							}
//							card.transform.position = player2CardInitialPosition;
//							Debug.Log ("Card Repositioned");
//						}
//
//					}
//					tappedCardsDictionary.Remove (touch.fingerId);
//				}
//			}
		}
	}
}
