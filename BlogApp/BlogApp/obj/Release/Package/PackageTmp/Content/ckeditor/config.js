/**
 * @license Copyright (c) 2003-2020, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
	config.syntaxhighlight_lang = 'csharp';
	config.syntaxhighlight_hideControls = true;
	config.languages = 'vi';
	config.filebrowserBrowseUrl = '/Content/ckfinder/ckfinder.html';
	config.filebrowserImageBrowseUrl = '/Content/ckfinder/ckfinder.html?Types=Images';
	config.filebrowserFlashBrowseUrl = '/Content/ckfinder/ckfinder.html?Types=Flash';
	config.filebrowserUploadUrl = '/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpLoad&type=File';
	config.filebrowserImageUploadUrl = '/images/upload/images';
	config.filebrowserFlashUploadUrl = '/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpLoad&type=Flash';

	CKFinder.setupCKEditor(null, '/Content/ckfinder/');
};
