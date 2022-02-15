/******************************************************************************
 * 
 * File: HealthBar.cs
 * Author: Joseph Crump
 * Date: 2/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Component that displays a character's health.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component that displays a character's health.
/// </summary>
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Image barImage;

    [SerializeField]
    [SerializeReference]
    [SerializePolymorphic]
    private FillMethod fillMethod;

    private void Start()
    {
        RegisterHealthChangedEvent();

        SetFillAmount(character.Health / character.Health.MaxValue);
    }

    private void OnDestroy()
    {
        DeregisterHealthChangedEvent();
    }

    protected void SetFillAmount(float fillAmount)
    {
        if (barImage == null)
            return;

        if (fillMethod == null)
        {
            Debug.LogWarning(
                $"{this} does not have a {nameof(fillMethod)} " +
                $"assigned. Unable to update {nameof(HealthBar)}");
            return;
        }

        fillAmount = Mathf.Clamp01(fillAmount);

        fillMethod.SetFillAmount(barImage, fillAmount);
    }

    private void RegisterHealthChangedEvent()
    {
        if (character == null)
            return;

        character.HealthChanged.AddListener(OnCharacterHealthChanged);
    }

    private void DeregisterHealthChangedEvent()
    {
        if (character == null)
            return;

        character.HealthChanged.RemoveListener(OnCharacterHealthChanged);
    }

    protected virtual void OnCharacterHealthChanged(object sender, VitalChangedEventArgs e)
    {
        float maxHealth = e.Vital.MaxValue;
        float currentHealth = e.CurrentValue;

        SetFillAmount(currentHealth / maxHealth);
    }
}
