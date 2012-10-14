using System;


    public static class ExtendDocumentReader
    {
        public static Maybe<TEntity> Get<TKey,TEntity>(this IDocumentReader<TKey,TEntity> self, TKey key)
        {
            TEntity entity;
            if (self.TryGet(key, out entity))
            {
                return entity;
            }
            return Maybe<TEntity>.Empty;
        }

        public static TEntity Load<TKey,TEntity>(this IDocumentReader<TKey,TEntity> self, TKey key)
        {
            TEntity entity;
            if (self.TryGet(key, out entity))
            {
                return entity;
            }
            var txt = string.Format("Failed to load '{0}' with key '{1}'.", typeof(TEntity).Name, key);
            throw new InvalidOperationException(txt);
        }

        public static TView GetOrNew<TView>(this IDocumentReader<unit, TView> reader)
            where TView : new()
        {
            TView view;
            if (reader.TryGet(unit.it, out view))
            {
                return view;
            }
            return new TView();
        }

        public static Maybe<TSingleton> Get<TSingleton>(this IDocumentReader<unit, TSingleton> reader)
        {
            TSingleton singleton;
            if (reader.TryGet(unit.it, out singleton))
            {
                return singleton;
            }
            return Maybe<TSingleton>.Empty;
        }
    
}