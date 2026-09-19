DELETE FROM brand_assets
WHERE "BrandId" = '8a0f1c2e-3b4d-4e5f-9a01-000000000001';

DELETE FROM product_brandings
WHERE "ProductId" IN (
    '8a0f1c2e-3b4d-4e5f-9a01-000000000021',
    '8a0f1c2e-3b4d-4e5f-9a01-000000000022',
    '8a0f1c2e-3b4d-4e5f-9a01-000000000023',
    '8a0f1c2e-3b4d-4e5f-9a01-000000000024',
    '8a0f1c2e-3b4d-4e5f-9a01-000000000025'
);

DELETE FROM products
WHERE "BrandId" = '8a0f1c2e-3b4d-4e5f-9a01-000000000001';

DELETE FROM brands
WHERE "Id" = '8a0f1c2e-3b4d-4e5f-9a01-000000000001';

DELETE FROM themes
WHERE "Id" = '8a0f1c2e-3b4d-4e5f-9a01-000000000010';
