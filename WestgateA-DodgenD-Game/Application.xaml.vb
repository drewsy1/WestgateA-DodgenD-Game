Class Application
#Region "Events"
    ''' <summary>
    ''' ToDo Write PressFireButton summary
    ''' </summary>
    Public Shared Event PressFireButton()

    ''' <summary>
    ''' ToDo Write PlayerProjectileRemove summary
    ''' </summary>
    ''' <param name="projectile"></param>
    Public Shared Event PlayerProjectileRemove(projectile As ProjectileClasses.ProjectilePlayer)

    ''' <summary>
    ''' ToDo Write ProjectileHit summary
    ''' </summary>
    ''' <param name="projectile"></param>
    ''' <param name="entity"></param>
    Public Shared Event ProjectileHit(ByRef projectile As ProjectileClasses.ProjectileBase, ByRef entity As Object)

    ''' <summary>
    ''' ToDo Write ReleaseFireButton summary
    ''' </summary>
    Public Shared Event ReleaseFireButton()

    ''' <summary>
    ''' ToDo Write EnemyHit summary
    ''' </summary>
    ''' <param name="enemy"></param>
    Public Shared Event EnemyHit(enemy As EntityClasses.EntityEnemyBase)
    #End Region

    #Region "Event Friend Functions"
    ''' <summary>
    ''' ToDo Write RaiseProjectileHit summary
    ''' </summary>
    ''' <param name="projectile"></param>
    ''' <param name="entity"></param>
    Friend Shared Sub RaiseProjectileHit(ByRef projectile As ProjectileClasses.ProjectileBase, ByRef entity As Object)
        RaiseEvent ProjectileHit(projectile, entity)
    End Sub

    ''' <summary>
    ''' ToDo Write RaiseReleaseFireButton summary
    ''' </summary>
    Friend Shared Sub RaiseReleaseFireButton()
        RaiseEvent ReleaseFireButton()
    End Sub

    ''' <summary>
    ''' ToDo Write RaisePressFireButton summary
    ''' </summary>
    Friend Shared Sub RaisePressFireButton()
        RaiseEvent PressFireButton()
    End Sub

    ''' <summary>
    ''' ToDo Write RaisePlayerProjectileRemove Summary
    ''' </summary>
    ''' <param name="projectile"></param>
    Friend Shared Sub RaisePlayerProjectileRemove(projectile As ProjectileClasses.ProjectilePlayer)
        RaiseEvent PlayerProjectileRemove(projectile)
    End Sub

    ''' <summary>
    ''' ToDo Write RaiseEnemyHit summary
    ''' </summary>
    ''' <param name="enemy"></param>
    Friend Shared Sub RaiseEnemyHit(enemy As EntityClasses.EntityEnemyBase)
        RaiseEvent EnemyHit(enemy)
    End Sub
    #End Region



    ''' <summary>
    ''' ToDo Write ProcessProjectileHit summary
    ''' </summary>
    ''' <param name="projectile"></param>
    ''' <param name="entity"></param>
    Public Shared Sub ProcessProjectileHit(ByRef projectile As ProjectileClasses.ProjectileBase, ByRef entity As Object)
        projectile.Remove()
        entity.Remove()
    End Sub
End Class
