using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookUI : MonoBehaviour, ITab
{
    [SerializeField]
    private SpellCaster spellCaster;

    [SerializeField]
    private bool _autoCast = true;

    [SerializeField]
    private Canvas spelbookCanvas;

    [SerializeField]
    public Button CancelSpellBookButton;

    [SerializeField]
    public Button CancelSpellButton;

    [SerializeField]
    private Button applySpell;

    [SerializeField]
    private Canvas _buttonsCanvas;

    private SpellButton[] spellButtons;

    private void Awake()
    {
        spellButtons = GetComponentsInChildren<SpellButton>();

        foreach (SpellButton spellButton in spellButtons)
        {
            spellButton.OnSpellSelected = SpellSelected;
        }

        applySpell.onClick.AddListener(() => spellCaster.CastSpell());
        spellCaster.SpellCasted += OnSpellCasted;
        CancelSpellButton.onClick.AddListener(() => CancelSelectedSpell());
    }

    private void CancelSelectedSpell()
    {
        spellCaster.ClearCasting();
        RefreshControls();
    }

    private void OnSpellCasted(Spell spell)
    {
        SpellButton spellButton = spellButtons.Where(btn => btn.Spell == spell).First();
        spellButton.StartCooldown();
        CancelSelectedSpell();
    }

    private void SpellSelected(Spell spell)
    {
        spellCaster.SetupSpell(spell);
        spellCaster.TargetAquired += SpellCaster_TargetAquired;
        RefreshControls();
    }

    private void SpellCaster_TargetAquired(GameObject obj)
    {
        applySpell.enabled = obj != null;
        if(_autoCast)
        {
            spellCaster.CastSpell();
            spellCaster.ClearCasting();
        }
        
    }

    private void RefreshControls()
    {
        bool castSpellActive = spellCaster.ActiveSpell != null;
        applySpell.gameObject.SetActive(castSpellActive && !_autoCast);
        CancelSpellBookButton.gameObject.SetActive(!castSpellActive);
        CancelSpellButton.gameObject.SetActive(castSpellActive);
    }

    public void Hide()
    {
        spelbookCanvas.gameObject.SetActive(false);
        _buttonsCanvas.gameObject.SetActive(false);
    }

    public void Show()
    {
        spelbookCanvas.gameObject.SetActive(true);
        _buttonsCanvas.gameObject.SetActive(true);
        RefreshControls();
    }
}
