const slugPattern = /^[a-z0-9]+(?:-[a-z0-9]+)*$/;

export const isValidSlug = (value: string): boolean => slugPattern.test(value.trim());

export const slugify = (value: string): string => {
  return value
    .trim()
    .toLowerCase()
    .replace(/[^a-z0-9]+/g, "-")
    .replace(/^-+|-+$/g, "");
};

export const required = (value: string, label: string): string | null => {
  return value.trim() ? null : `${label} is required.`;
};
