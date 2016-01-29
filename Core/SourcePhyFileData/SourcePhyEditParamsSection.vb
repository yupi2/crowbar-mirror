Public Class SourcePhyEditParamsSection

	'fprintf( fp, "editparams {\n" );
	'KeyWriteString( fp, "rootname", g_JointedModel.m_rootName );
	'KeyWriteFloat( fp, "totalmass", g_JointedModel.m_totalMass );

	' Example: 
	'editparams {
	'"rootname" "valvebiped.bip01_pelvis"
	'"totalmass" "100.000000"
	'}

	Public concave As String
	Public rootName As String
	Public totalMass As Single

End Class
