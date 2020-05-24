
















































































/*

Sum:

            var przelewy = new Query.Table(nameof(Przelewy));

            przelewy += new Query.Field(nameof(Przelew.Podmiot))
            { PropertyName = nameof(SumResult.PodmiotID) };
            przelewy += new Query.Sum(nameof(Przelew.Kwota));

            return new List<SumResult>(Session.Execute<SumResult>(przelewy));

Join:

            var przelewy = new Query.Table(nameof(Przelewy));
            var changeInfos = new Query.InnerJoin(nameof(ChangeInfos),
                new FieldCondition.Equal(nameof(ChangeInfo.SourceGuid), przelewy, nameof(PrzelewBase.Guid))
                & new FieldCondition.Equal(nameof(ChangeInfo.Type), ChangeInfoType.Created));
            var operators = new Query.InnerJoin(nameof(Operators),
                new FieldCondition.Equal(nameof(Operator.ID), changeInfos, nameof(ChangeInfo.Operator)));

            przelewy += new Query.Field(nameof(PrzelewBase.NazwaOdbiorcy1)) { PropertyName = nameof(JoinResult.Odbiorca) };
            przelewy += new Query.Field(nameof(PrzelewBase.Kwota)) { PropertyName = nameof(JoinResult.Kwota) };
            operators += new Query.Field(nameof(Operator.Name)) { PropertyName = nameof(JoinResult.Operator) };

            changeInfos.Add(operators);
            przelewy.Add(changeInfos);

            return new List<JoinResult>(Session.Execute<JoinResult>(przelewy));

*/
