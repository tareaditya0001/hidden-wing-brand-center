# White-label guide

Hidden Wing applications should treat Brand Center as the only mutable branding source.

## Model

```text
Global brand
  → Product
    → Product branding override
      → Application
```

Example:

```text
Hidden Wing
  → Hidden Wing Store
    → Store-specific purple primary
      → Store application
```

If Store does not set a font, it receives the global theme font.

## Application checklist

1. Store the Brand Center base URL in environment config.
2. Fetch `/api/v1/public/branding/{slug}` at startup or on a short cache interval.
3. Map `theme` values to CSS variables.
4. Use `product.logo` and `product.favicon` instead of checked-in product marks when overrides exist.
5. Keep a local fallback only for offline or first paint.

## What not to do

- Do not duplicate branding rows in another database.
- Do not hardcode production product colors in application source.
- Do not call admin APIs from customer-facing apps.
