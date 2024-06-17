using Content.Shared.Standing;
using Content.Server.Body.Systems;
using Content.Shared.Damage;

namespace Content.Server.Administration.Systems;

public sealed class EggVerbSystem : EntitySystem
{
    [Dependency] private readonly BodySystem _body = default!;
    public override void Initialize()
    {
        SubscribeLocalEvent<EggVerbComponent, DownedEvent>(OnDown);
        SubscribeLocalEvent<EggVerbComponent, DamageChangedEvent>(OnDamaged);
    }

    private void OnDown(Entity<EggVerbComponent> ent, ref DownedEvent args)
    {
        _body.GibBody(ent.Owner);
    }

    private void OnDamaged(Entity<EggVerbComponent> ent, ref DamageChangedEvent args)
    {
        if (args.DamageDelta is null || !args.DamageIncreased)
        {
            return;
        }
        _body.GibBody(ent.Owner);
    }
}
