import { HealthAppTemplatePage } from './app.po';

describe('HealthApp App', function() {
  let page: HealthAppTemplatePage;

  beforeEach(() => {
    page = new HealthAppTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
