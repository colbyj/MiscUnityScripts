using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEntry 
{
    public string text;
    public float duration;
    private float timeBegun = -1;

    public DialogEntry(string text, float duration = 0.0f) 
    {
        this.text = text;
        this.duration = duration;
    }

    public void BeginDialogVisible()
    {
        this.timeBegun = Time.time;
        //Debug.Log("Time begun: " + timeBegun);
    }

    public bool IsDialogOver()
    {
        if (timeBegun == -1)
        {
            return false;
        }
        return (timeBegun + duration < Time.time);
    }
}

public class DialogSystem : MonoBehaviour 
{
    public UnityEngine.UI.Text txtDialog;
    public UnityEngine.UI.Image imgBackground;

    private Queue<DialogEntry> queue = new Queue<DialogEntry>();
    private bool showingDialog = false;

	void Start () 
    {
        SetBGAlpha(0.0f);
        SetTextAlpha(0.0f);
	}
	
	void Update () 
    {
		if (queue.Count == 0)
        { 
            SetTextAlpha(0.0f);
            SetBGAlpha(0.0f);

            return;
        }

        //Debug.Log("Queue count: " + queue.Count.ToString());
        //Debug.Log("!showingDialog = " + !showingDialog + "; queue.Count > 0 = " + (queue.Count > 0).ToString() + "; queue.Peek().IsDialogOver() = " + queue.Peek().IsDialogOver());

        if (!showingDialog && queue.Count > 0) 
        {
            SetTextAlpha(0.0f);
            SetBGAlpha(0.5f);

            DialogEntry next = queue.Peek();
            txtDialog.text = next.text;
            next.BeginDialogVisible();

            Animation anim = txtDialog.GetComponent<Animation>();
            anim.Play();

            showingDialog = true;
        }
        else if (queue.Peek().IsDialogOver() && queue.Count > 0)
        {
            DialogEntry oldEntry = queue.Peek();

            // If duration was set to 0, don't hide anything. Hide it only if there are more than one items in the queue.
            if (oldEntry.duration != 0.0f || queue.Count > 1)
            {
                queue.Dequeue();
                showingDialog = false;

                SetTextAlpha(0.0f);
                SetBGAlpha(0.0f);
            }            
        }
	}

    private void SetBGAlpha(float alpha)
    {
        Color color = imgBackground.color;
        color.a = alpha;
        imgBackground.color = color;
    }

    private void SetTextAlpha(float alpha)
    {
        Color color = txtDialog.color;
        color.a = alpha;
        txtDialog.color = color;
    }

    /// <summary>
    /// Add text to the queue. 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="duration">if 0, the text won't be removed from the queue unless there are other text entries in it.</param>
    public void AppendText(string text, float duration = 0.0f)
    {
        queue.Enqueue(new DialogEntry(text, duration));
    }

    /// <summary>
    /// Remove all of the text currently in the queue.
    /// </summary>
    public void Clear()
    {
        queue.Clear();
        SetBGAlpha(0.0f);
        SetTextAlpha(0.0f);
        showingDialog = false;
    }
}
