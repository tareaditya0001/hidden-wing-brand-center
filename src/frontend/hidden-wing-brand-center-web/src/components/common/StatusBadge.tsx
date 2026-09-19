interface StatusBadgeProps {
  active: boolean;
}

export const StatusBadge = ({ active }: StatusBadgeProps) => {
  return <span className={active ? "badge" : "badge inactive"}>{active ? "Active" : "Inactive"}</span>;
};
