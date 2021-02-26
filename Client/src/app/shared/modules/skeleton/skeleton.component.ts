import { Component, ChangeDetectionStrategy, Input } from '@angular/core';

@Component({
  selector: 'app-skeleton',
  templateUrl: './skeleton.component.html',
  styleUrls: ['./skeleton.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SkeletonComponent {

  @Input() width: string;
  @Input() height: string;
  @Input() circle: string;

  getStyles() {
    const styles = {
      'width.px': this.width ? this.width : '',
      'height.px': this.height ? this.height : '',
      'border-radius': this.circle === 'true' ? '50%' : '',
    };
    return styles;
  }
}