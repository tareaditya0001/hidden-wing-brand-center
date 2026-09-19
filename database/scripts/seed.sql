INSERT INTO themes (
    "Id", "Name", "Slug", "PrimaryColor", "SecondaryColor", "AccentColor", "BackgroundColor",
    "SurfaceColor", "TextColor", "MutedTextColor", "BorderColor", "FontFamily", "BorderRadius",
    "IsDefault", "CreatedAt", "UpdatedAt"
)
VALUES (
    '8a0f1c2e-3b4d-4e5f-9a01-000000000010',
    'Hidden Wing Default',
    'hidden-wing-default',
    '#1C3353',
    '#3E536B',
    '#C6A15B',
    '#F4EFE6',
    '#FFFFFF',
    '#1A1F29',
    '#5C6B7A',
    '#D9D2C5',
    'Inter, system-ui, sans-serif',
    '8px',
    TRUE,
    '2026-01-01 00:00:00+00',
    '2026-01-01 00:00:00+00'
)
ON CONFLICT ("Id") DO NOTHING;

INSERT INTO brands ("Id", "Name", "Slug", "CompanyName", "Description", "IsActive", "CreatedAt", "UpdatedAt")
VALUES (
    '8a0f1c2e-3b4d-4e5f-9a01-000000000001',
    'Hidden Wing',
    'hidden-wing',
    'Hidden Wing',
    'Technology, products, solutions and digital ventures built under Hidden Wing.',
    TRUE,
    '2026-01-01 00:00:00+00',
    '2026-01-01 00:00:00+00'
)
ON CONFLICT ("Id") DO NOTHING;

INSERT INTO products ("Id", "BrandId", "Name", "Slug", "Description", "IsActive", "Status", "CreatedAt", "UpdatedAt")
VALUES
    ('8a0f1c2e-3b4d-4e5f-9a01-000000000021', '8a0f1c2e-3b4d-4e5f-9a01-000000000001', 'Hidden Wing HQ', 'hq', 'Internal operations and company headquarters workspace.', TRUE, 'Active', '2026-01-01 00:00:00+00', '2026-01-01 00:00:00+00'),
    ('8a0f1c2e-3b4d-4e5f-9a01-000000000022', '8a0f1c2e-3b4d-4e5f-9a01-000000000001', 'Hidden Wing Store', 'store', 'Commerce and catalog experience for Hidden Wing products.', TRUE, 'Active', '2026-01-01 00:00:00+00', '2026-01-01 00:00:00+00'),
    ('8a0f1c2e-3b4d-4e5f-9a01-000000000023', '8a0f1c2e-3b4d-4e5f-9a01-000000000001', 'Hidden Wing Projects', 'projects', 'Project delivery and collaboration workspace.', TRUE, 'Active', '2026-01-01 00:00:00+00', '2026-01-01 00:00:00+00'),
    ('8a0f1c2e-3b4d-4e5f-9a01-000000000024', '8a0f1c2e-3b4d-4e5f-9a01-000000000001', 'Hidden Wing Data', 'data', 'Data platform and reporting workspace.', TRUE, 'Active', '2026-01-01 00:00:00+00', '2026-01-01 00:00:00+00'),
    ('8a0f1c2e-3b4d-4e5f-9a01-000000000025', '8a0f1c2e-3b4d-4e5f-9a01-000000000001', 'Hidden Wing Admin', 'admin', 'Administrative console for Hidden Wing operators.', TRUE, 'Active', '2026-01-01 00:00:00+00', '2026-01-01 00:00:00+00')
ON CONFLICT ("Id") DO NOTHING;
