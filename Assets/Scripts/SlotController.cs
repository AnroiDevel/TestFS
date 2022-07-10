using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace Test
{
    public class SlotController : MonoBehaviour
    {
        [SerializeField] private Sprite[] _slot1Elements;
        [SerializeField] private Sprite[] _slot2Elements;

        [SerializeField] private Rigidbody2D[] _slot1Rb;
        [SerializeField] private Rigidbody2D[] _slot2Rb;

        [SerializeField] private Button _playBtn;
        [SerializeField] private Text _scoreText;
        [SerializeField] private int _score = 100;


        private void Start()
        {
            if (_score <= 0)
                _score = 100;
            _scoreText.text = _score.ToString();
        }

        public void StartSlots()
        {
            _score -= 10;
            _scoreText.text = _score.ToString();

            _playBtn.interactable = false;

            var forse = Random.Range(1000, 2000);

            foreach (var rb in _slot1Rb)
                rb.AddForce(Vector2.right * forse);

            foreach (var rb in _slot2Rb)
                rb.AddForce(Vector2.left * forse);


            StartCoroutine(SlotsRotate());
        }

        private IEnumerator SlotsRotate()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                foreach (var rb in _slot1Rb)
                {
                    if (rb.transform.localPosition.x >= _slot1Rb.Length * 2)
                    {
                        rb.transform.localPosition = new Vector2(rb.transform.localPosition.x - _slot1Rb.Length * 2, rb.transform.localPosition.y);
                    }
                }
                foreach (var rb in _slot2Rb)
                {
                    if (rb.transform.localPosition.x >= _slot2Rb.Length * 2)
                    {
                        rb.transform.localPosition = new Vector2(rb.transform.localPosition.x - _slot2Rb.Length * 2, rb.transform.localPosition.y);
                    }
                }

                if (_slot1Rb[0].velocity.x < 0.1f)
                {
                    foreach (var rb in _slot1Rb)
                    {
                        var target = new Vector2(Mathf.Round(rb.transform.localPosition.x), Mathf.Round(rb.transform.localPosition.y));
                        rb.transform.localPosition = Vector2.MoveTowards(rb.transform.localPosition, target, 0.1f);
                    }
                    foreach (var rb in _slot2Rb)
                    {
                        var target = new Vector2(Mathf.Round(rb.transform.localPosition.x), Mathf.Round(rb.transform.localPosition.y));
                        rb.transform.localPosition = Vector2.MoveTowards(rb.transform.localPosition, target, 0.1f);
                    }
                }

                if (_slot1Rb[1].velocity.x <= 0)
                {
                    var win = false;

                    foreach (var rb in _slot1Rb)
                    {
                        if (rb.transform.localPosition.x == 7)
                        {
                            win = true;
                            break;
                        }
                    }
                    if (win)
                    {
                        _score += 20;
                        _scoreText.text = _score.ToString();
                    }
                    _playBtn.interactable = true;
                    yield break;
                }
            }

        }

    }

}