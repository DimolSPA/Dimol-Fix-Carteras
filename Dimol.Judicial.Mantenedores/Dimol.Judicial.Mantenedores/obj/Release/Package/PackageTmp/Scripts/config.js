/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For complete reference see:
	// http://docs.ckeditor.com/#!/api/CKEDITOR.config

	// The toolbar groups arrangement, optimized for two toolbar rows.
    config.toolbarGroups = [
        { name: 'document', groups: ['document', 'doctools'] },
		{ name: 'clipboard',   groups: [ 'clipboard', 'undo' ] },
		{ name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ] },
		{ name: 'links' },
		{ name: 'insert' },
		'/',
		{ name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
        { name: 'tools' },
		{ name: 'styles' },
		{ name: 'colors' }
	];
	
	config.language = 'es';
    config.height = 550;
    config.toolbarCanCollapse = true;
    config.allowedContent = true;
    config.extraAllowedContent = "table[class]";
    config.startupShowBorders = false;
    config.extraPlugins = 'indentblock';

	//// Remove some buttons provided by the standard plugins, which are
	//// not needed in the Standard(s) toolbar.
	//config.removeButtons = 'Underline,Subscript,Superscript';

	//// Set the most common block elements.
	//config.format_tags = 'p;h1;h2;h3;pre';

	//// Simplify the dialog windows.
	//config.removeDialogTabs = 'image:advanced;link:advanced';

	//removePlugins= 'bidi,font,forms,flash,horizontalrule,iframe,justify,table,tabletools,smiley';
	//removeButtons = 'Anchor,Underline,Strike,Subscript,Superscript,Image';
	//format_tags = 'p;h1;h2;h3;pre;address';
};
