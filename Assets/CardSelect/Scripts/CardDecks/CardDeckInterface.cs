using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Interface for card deck
 */ 
[DisallowMultipleComponent]
public abstract class CardDeckInterface : MonoBehaviour {

	public enum SORTING_MODE
	{
		ASCENDING,
		DESCENDING
	}

	const float CAMERA_TO_CARD_DISTANCE = 2.0f;

	// Hover time before tooltip shown
	[Tooltip("How the card is sorted")]
	public SORTING_MODE sortingMode = SORTING_MODE.DESCENDING;

	// Hover time before tooltip shown
	[Tooltip("Hover time before tooltip shown")]
	public float tooltipTriggerTime = 2.0f;

	// Spacing between cards
	[Tooltip("Spacing between cards")]
	public float cardSpacing = 1.0f;

	// Intro duration in Seconds
	[Tooltip("Duration of intro animation")]
	[SerializeField]
	private float introDuration = 1.0f;

	// Card move speed
	[Tooltip("Card move speed")]
	public float cardMoveSpeed = 1.0f;

	// Starting Index
	[Tooltip("Starting card index")]
	[SerializeField]
	private int startingIndex = 0;

	// cards holder for inspector
	[Tooltip("Put your cards here")]
	[SerializeField]
	private List<Card> _cards;

	// inspector cards count
	protected int cardsCount;

	// real variable used
	protected List<Card> cards;

	// set wether the carddeck is a continous one
	protected bool loop = false;

	// cardDeck X Size, calculated from card counts & card spacing
	protected float sizeX;

	// internal valiable, Curve offset 
	protected float offsett;

	// current card / selected card index
	protected int index;


	/*
     * Internal variable for detecting swipe
     */
	private bool isSwiping;
	private Vector3 touchBeginPoint;
	private float startingOffsett;


	/*
     * Internal variable for showing tooltip
     */
	private float tooltipDetectTimer;

	/*
     * ======================================================================
     * Abstract Methods, MUST BE IMPLEMENTED
     * ======================================================================
     */
	// init() called after start() function
	protected abstract void Init();

	// function describing how cards are arranged 
	protected abstract void ReArrangeCards();
	/*
     * ======================================================================
     */

	private bool isAnimating;

	/*
     * Unity Start function
     */ 
	private void Start()
	{
		cards = new List<Card>(_cards.Count);

		// calculating deck size
		sizeX = (_cards.Count) * cardSpacing;
		index = 0; offsett = 0.0f;

		Init();

		isAnimating = false;
        isSwiping = false;

		StartCoroutine(IntroAnimation());

	}

	/*
     * Main Update function
     */ 
	private void Update()
	{
		foreach (Card card in cards)
			if(card != null) card.transform.LookAt(Camera.main.transform.position);

		ReArrangeCards();

		ListenInput();

		if (!isSwiping)
			IndexUpdate();
	}



	/*
     * CardDeck intro animation, add card one by one
     */
	private IEnumerator IntroAnimation()
	{
		isAnimating = true;

		for (int i = 0; i < _cards.Count; i++)
		{
			yield return new WaitForSeconds(introDuration / _cards.Count);
			AddCard(_cards[i]);
		}

		index = startingIndex;
		isAnimating = false;
	}



	/*
     * Listen to touch swipe or mouse drage
     */
	private void ListenInput()
	{
		// if mouse scroll changed, change the index
		index += (int)Input.mouseScrollDelta.y;

		if (Input.touchCount > 0)
		{
			// only touch 0 is used
			Touch touch = Input.GetTouch(0);

			Ray ray = Camera.main.ScreenPointToRay(touch.position);
			RaycastHit hit;

			if (touch.phase == TouchPhase.Began)
			{
				touchBeginPoint = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, CAMERA_TO_CARD_DISTANCE));
				startingOffsett = offsett * cardSpacing;
			}

			if (touch.phase == TouchPhase.Moved)
			{
				Vector2 currentTouchPoint = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, CAMERA_TO_CARD_DISTANCE));
				offsett = (startingOffsett + (currentTouchPoint.x - touchBeginPoint.x)) / cardSpacing;

				if (!loop)
				{
					if (offsett > (_cards.Count-1)) offsett = _cards.Count-1;
					if (offsett < 0) offsett = 0;
				}

				isSwiping = true;

				index = (int)Mathf.Round(offsett);
			}

			if (touch.phase == TouchPhase.Ended)
			{
				if (!isSwiping)
				{
					if (Physics.Raycast(ray, out hit))
					{
						Card card = hit.collider.gameObject.GetComponent<Card>();
						index = card.id;
					}
				}

				isSwiping = false;
			}
		}
		else
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Input.GetMouseButtonDown(0))
			{
				touchBeginPoint = Camera.main.ScreenToWorldPoint(
					new Vector3(Input.mousePosition.x, Input.mousePosition.y, CAMERA_TO_CARD_DISTANCE));
				startingOffsett = offsett * cardSpacing;
			}

			if (Input.GetMouseButton(0))
			{
				Vector3 currentTouchPoint = Camera.main.ScreenToWorldPoint(
					new Vector3(Input.mousePosition.x, Input.mousePosition.y, CAMERA_TO_CARD_DISTANCE));

				if ((currentTouchPoint - touchBeginPoint).sqrMagnitude > 0.01f)
				{
					offsett = (startingOffsett + (currentTouchPoint.x - touchBeginPoint.x)) / cardSpacing;

					if (!loop)
					{
						if (offsett > (_cards.Count-1)) offsett = _cards.Count-1;
						if (offsett < 0) offsett = 0;
					}

					isSwiping = true;

					index = (int)Mathf.Round(offsett);
				}
			}

			if (Input.GetMouseButtonUp(0))
			{
				if (!isSwiping)
				{
					if (Physics.Raycast(ray, out hit))
					{
						Card card = hit.collider.gameObject.GetComponent<Card>();
						index = card.id;
					}
				}

				isSwiping = false;
			}

			/*
             * Show tooltip based on tooltip timer
             */
			if (CardTooltip.instance != null)
			{
				if (Physics.Raycast(ray, out hit))
				{
					Card card = hit.collider.gameObject.GetComponent<Card>();

					if (card.id == index)
					{
						tooltipDetectTimer += Time.deltaTime;
						if (tooltipDetectTimer > tooltipTriggerTime)
						{
							CardTooltip.instance.SetMessage(card.tooltipMessage);
							CardTooltip.instance.gameObject.SetActive(true);
							CardTooltip.instance.transform.position = Input.mousePosition;

						}
					}
					else
					{
						CardTooltip.instance.gameObject.SetActive(false);
						tooltipDetectTimer = 0;
					}
				}
				else
				{
					CardTooltip.instance.gameObject.SetActive(false);
					tooltipDetectTimer = 0;
				}
			}

		}

		if (index < 0)	index += _cards.Count;
		if (index > _cards.Count - 1)	index -= _cards.Count;
		index %= _cards.Count;
	}

	/*
     * Listening to index change and interpolate the offsett to match the index
     * This code called every frame in Update function
     * Make this functon virtual, so every class can use their own interpolation 
     */
	protected virtual void IndexUpdate()
	{

		if (index < 0) index = 0;
		if (index > cards.Count - 1) index = cards.Count - 1;

		float target = 0;
		if (sortingMode == SORTING_MODE.DESCENDING)
			target = index; 
		else if (sortingMode == SORTING_MODE.ASCENDING)
			target = (cards.Count - 1) - index;

		if (offsett != target)
		{
			if (offsett < target)
			{
				offsett = Mathf.Lerp(offsett, target, 0.1f * cardMoveSpeed);

				if (target - offsett < 0.01f)
					offsett = target;
			}

			if (offsett > target)
			{
				offsett = Mathf.Lerp(offsett, target, 0.1f * cardMoveSpeed);

				if (offsett - target < 0.01f)
					offsett = target;
			}
		}
		else
		{
			offsett = target;
		}
	}

	/*
     * Move cards from inspector to internal List cards
     * we can do this with animations
     * note : we still want to handle null valued card in inspector
     *        assuming that is a space
     */ 
	private void AddCard(Card inspectorCard)
	{
		Card card = null;

		if (inspectorCard != null)
		{
			card = Card.Instantiate<Card>(inspectorCard);
			card.transform.SetParent(transform, true);
			card.transform.position = Vector3.one * 999999;
			card.id = _cards.IndexOf(inspectorCard);
		}

		cards.Add(card);

		if (sortingMode == SORTING_MODE.DESCENDING)
			index = cards.Count - 1;
		else 
		{
			index = 0;
			cards.Sort ();
			cards.Reverse ();
		}


	}

	// Current / selected card index
	public int GetCurrentIndex()
	{
		return index;
	}

	// Set selected card index
	public void SetCurrentIndex(int value)
	{
        if(loop)
        {
            if (value < 0)
                value = _cards.Count - 1;

            if (value > _cards.Count - 1)
                value = 0;
        }
        
        if(value >= 0 && value < _cards.Count) 
			    index = value;
	}

	// get card size / count
	public int Size()
	{
		return _cards.Count;
	}

	// get card based on id
	public Card GetCard(int id)
	{
		if (!isAnimating)
		{
			if (id > 0 && id < _cards.Count)
				return _cards[id];
		}

		return null;
	}

	// get current card,
	public Card GetCurrentCard()
	{
		if(!isAnimating)
			return _cards[index];

		return null;
	}
}
